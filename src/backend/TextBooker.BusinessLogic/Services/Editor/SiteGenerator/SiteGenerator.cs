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
		) : base(logger, db)
		{
			this.db = db;
			this.fileStoreSettings = fileStoreSettings;
		}

		public async Task<Result<bool>> Generate(string siteId, string userId)
		{
			var (_, isFailure, site, error) = await Validate(siteId, userId)
				.Bind(async () => await FindSite(siteId, userId))
				.OnFailure(LogError);

			var sitePath = Path.Combine(fileStoreSettings.BasePath, "sites", siteId);
			var template = await db.Templates.FirstOrDefaultAsync(x => x.Id == site.TemplateId);
			var templatePath = Path.Combine(fileStoreSettings.BasePath, "themes", template.Name, "tocopy");

			var templateProccessing = ClearSiteFolder(sitePath, templatePath)
				.Bind(() => CopyTemplate(sitePath, templatePath))
				.OnFailure(LogError);

			var preparedBlocks = GeneratePreparedBlocks(template, site.Blocks);
			var preparedTemplate = await GeneratePreparedTemplate(site, templatePath, site.SectionNames, preparedBlocks);

			// TODO: Pages generate

			// TODO: Add menu type option for page: main menu or aside menu, or together

			return Result.Ok(true);
		}

		private async Task<string> GeneratePreparedTemplate(
			Site site,
			string templatePath,
			ICollection<SectionName> sectionNames,
			string preparedBlocks			
		)
		{
			var html = await File.ReadAllTextAsync($"{templatePath}/blank.html");

			var menu = GetMenu(site.Pages);
			html = html.Replace($"%MainMenu%", menu);
			html = html.Replace($"%SidebarMenu%", menu);

			html = html.Replace($"%SiteTitle%", site.Title);
			html = html.Replace($"%Blocks%", preparedBlocks);

			var templateKeys = await db.TemplateKeys.ToListAsync();
			var replaces = sectionNames.Join(templateKeys, s => s.TemplateKeyId, t => t.Id, (s, t) => new { t.Name, s.Content } );

			foreach (var item in replaces)
			{
				html = html.Replace($"%{item.Name}%", item.Content);
			}

			return html;

			static string GetMenu(ICollection<Page> pages)
			{
				var menu = pages.Select(p => $"<li><a href=\"{p.Alias}.html\">{p.Title}</a></li>");
				return string.Join("", menu);
			}
		}

		private string GeneratePreparedBlocks(Template template, ICollection<Block> blocks)
		{
			var html = new StringBuilder();

			foreach (var block in blocks)
			{
				html.Append(template.BlockBegin);
				html.Append($"{template.BlockTitleBegin} {block.Title} {template.BlockTitleEnd}");
				html.Append($"{template.BlockContentBegin} {block.Content} {template.BlockContentEnd}");
				html.Append(template.BlockEnd);
			}

			return html.ToString();
		}

		private Result CopyTemplate(string sitePath, string templatePath)
		{
			try
			{
				DirectoryCopy(templatePath, sitePath, true);
				return Result.Ok();
			}
			catch (Exception ex)
			{
				return Result.Failure("An error occurred when copying the directory.");
			}
		}

		private Result ClearSiteFolder(string sitePath, string templatePath)
		{
			var templateFiles = Directory.GetFiles(templatePath, "*", SearchOption.AllDirectories);

			var deletedFiles = 0;
			foreach (var item in templateFiles)
			{
				var filepath = item.Replace(templatePath, sitePath);

				if (File.Exists(filepath))
				{
					File.Delete(filepath);
					deletedFiles++;
				}
			}

			return deletedFiles == templateFiles.Count()
				? Result.Ok()
				: Result.Failure($"An error occurred while clearing the site folder");
		}

		private async Task<Result<Site>> FindSite(string siteId, string userId)
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

		private static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
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
				file.CopyTo(temppath, false);
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
}

