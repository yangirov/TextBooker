using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CSharpFunctionalExtensions;

using Microsoft.EntityFrameworkCore;

using Serilog;

using TextBooker.Common.Config;
using TextBooker.DataAccess;
using TextBooker.DataAccess.Entities;

namespace TextBooker.BusinessLogic.Services
{
	public class SiteGenerator : BaseService, ISiteGenerator
	{
		private readonly TextBookerContext db;
		private readonly FileStoreSettings fileStoreSettings;

		public SiteGenerator(
			ILogger logger,
			TextBookerContext db,
			FileStoreSettings fileStoreSettings
		) : base(logger)
        {
			this.db = db;
			this.fileStoreSettings = fileStoreSettings;
		}

		public async Task<Result<bool>> Generate(string siteId, string userId)
		{
			var (_, isFailure, site, error) = await Validate(siteId, userId)
				.Bind(async () => await GetSite(siteId, userId))
				.OnFailure(LogError);

			if (isFailure)
				return Result.Failure<bool>(error);

			var sitePath = GetSitePath(siteId);
			var template = GetTemplate(site.TemplateId);
			var templatePath = GetTemplatePath(template.Name);

			return await ClearSiteFolder(sitePath)
				.Bind(() => CopyTemplateFiles(sitePath, templatePath))
				.Bind(() => BuildBlocks(template, site.Blocks))
				.Bind(async (preparedBlocks) => await BuildTemplate(site, templatePath, site.SectionNames, preparedBlocks))
				.Bind(async (preparedTemplate) => await GeneratePages(site, sitePath, preparedTemplate))
				.OnFailure(LogError);
		}

		private async Task<Result<bool>> GeneratePages(Site site, string sitePath, string preparedTemplate)
		{
			var tasks = site.Pages.ToList().Select(async page =>
			{
				var pageHtml = BuildPage(page, site.Title, preparedTemplate);

				var pagePath = $"{sitePath}/{page.Alias}.html";
				if (!File.Exists(pagePath))
				{
					var file = File.Create(pagePath);
					file.Close();
				}

				await File.WriteAllTextAsync(pagePath, pageHtml, Encoding.UTF8);

				return Result.Ok();
			});

			var results = await Task.WhenAll(tasks);

			var (_, isFailure) = Result.Combine(results);
			return isFailure
				? Result.Failure<bool>("An error occurred when generating pages")
				: Result.Ok(true);

			static string BuildPage(Page page, string siteTitle, string preparedTemplate)
			{
				var html = preparedTemplate.Replace("%WindowTitle%", $"{page.Title} | {siteTitle}");
				html = html.Replace("%PageTitle%", page.Title);
				html = html.Replace("%PageContent%", page.Content);
				return html;
			}
		}

		private async Task<Result<string>> BuildTemplate(Site site, string templatePath, ICollection<SectionName> sectionNames, string preparedBlocks)
		{
			var html = await File.ReadAllTextAsync($"{templatePath}/blank.html");

			html = html.Replace($"%MainMenu%", GetMenu(site.Pages.Where(x => x.InMainMenu).ToArray()));
			html = html.Replace($"%SidebarMenu%", GetMenu(site.Pages.Where(x => x.InAsideMenu).ToArray()));

			html = html.Replace($"%SiteTitle%", site.Title);
			html = html.Replace($"%Blocks%", preparedBlocks);

			var templateKeys = await db.TemplateKeys.ToListAsync();
			var replaces = sectionNames.Join(templateKeys, s => s.TemplateKeyId, t => t.Id, (s, t) => new { t.Name, s.Content });

			foreach (var item in replaces)
			{
				html = html.Replace($"%{item.Name}%", item.Content);
			}

			return Result.Ok(html);

			static string GetMenu(ICollection<Page> pages)
			{
				var menu = pages.Select(p => $"<li><a href=\"{p.Alias}.html\">{p.Title}</a></li>");
				return string.Join("", menu);
			}
		}

		private Result<string> BuildBlocks(Template template, ICollection<Block> blocks)
		{
			var html = new StringBuilder();

			foreach (var block in blocks)
			{
				html.Append(template.BlockBegin);
				html.Append($"{template.BlockTitleBegin} {block.Title} {template.BlockTitleEnd}");
				html.Append($"{template.BlockContentBegin} {block.Content} {template.BlockContentEnd}");
				html.Append(template.BlockEnd);
			}

			return Result.Ok(html.ToString());
		}

		private Result CopyTemplateFiles(string sitePath, string templatePath)
		{
			try
			{
				DirectoryCopy(templatePath, sitePath, true);
				return Result.Ok();
			}
			catch (Exception)
			{
				return Result.Failure("An error occurred when copying the directory.");
			}

			static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
			{
				var dir = new DirectoryInfo(sourceDirName);

				if (!dir.Exists)
				{
					throw new DirectoryNotFoundException(
						"Source directory does not exist or could not be found: "
						+ sourceDirName);
				}

				var dirs = dir.GetDirectories();
				if (!Directory.Exists(destDirName))
				{
					Directory.CreateDirectory(destDirName);
				}

				var files = dir.GetFiles();
				foreach (var file in files)
				{
					var temppath = Path.Combine(destDirName, file.Name);
					file.CopyTo(temppath, true);
				}

				if (copySubDirs)
				{
					foreach (var subdir in dirs)
					{
						var temppath = Path.Combine(destDirName, subdir.Name);
						DirectoryCopy(subdir.FullName, temppath, copySubDirs);
					}
				}
			}
		}

		private Result ClearSiteFolder(string sitePath)
		{
			try
			{
				var folders = Directory.GetDirectories(sitePath, "*", SearchOption.TopDirectoryOnly);

				foreach (var folder in folders)
				{
					if (Directory.Exists(folder) && !folder.Contains("assets"))
					{
						Directory.Delete(folder, true);
					}
				}

				return Result.Ok();
			}
			catch (Exception)
			{
				return Result.Failure($"An error occurred while clearing the site folder");
			}
		}

		private string GetTemplatePath(string templateName) => Path.Combine(fileStoreSettings.BasePath, "themes", templateName, "tocopy");

		private Template GetTemplate(int templateId) => db.Templates.FirstOrDefault(x => x.Id == templateId);

		private string GetSitePath(string siteId) => Path.Combine(fileStoreSettings.BasePath, "sites", siteId);

		private async Task<Result<Site>> GetSite(string siteId, string userId)
		{
			var site = await db.Sites
				.Where(x => x.Id == siteId && x.UserId == userId)
					.Include(x => x.UserScripts)
					.Include(x => x.SectionNames)
					.Include(x => x.Pages)
					.Include(x => x.Blocks)
				.FirstOrDefaultAsync() ?? Maybe<Site>.None;

			return site.HasNoValue
				? Result.Failure<Site>("Site not found")
				: Result.Ok(site.Value);
		}

		private Result Validate(string siteId, string userId)
		{
			return string.IsNullOrEmpty(siteId) && string.IsNullOrEmpty(userId)
				? Result.Failure("Validation error, one or more fields are empty")
				: Result.Ok();
		}
	}
}

