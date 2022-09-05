using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using CSharpFunctionalExtensions;

using Microsoft.EntityFrameworkCore;

using Serilog;

using TextBooker.Contracts.Dto;
using TextBooker.DataAccess;
using TextBooker.DataAccess.Entities;

namespace TextBooker.BusinessLogic.Services
{
	public class SiteSettingsService : BaseService, ISiteSettingsService
	{
		private readonly IMapper mapper;
		private readonly TextBookerContext db;
		private readonly ISiteGenerator siteGenerator;

		public SiteSettingsService(
			IMapper mapper,
			ILogger logger,
			TextBookerContext db,
			ISiteGenerator siteGenerator
		) : base(logger)
        {
			this.mapper = mapper;
			this.db = db;
			this.siteGenerator = siteGenerator;
        }

		public async Task<Result<string>> Create(SiteDto dto)
		{
			return await Map(dto)
				.Bind(Create)
				.OnFailure(LogError);

			Result<Site> Map(SiteDto site) => Result.Success(mapper.Map<Site>(site));

			async Task<Result<string>> Create(Site entity)
			{
				db.Sites.Add(entity);
				await db.SaveChangesAsync();

				return Result.Success(entity.Id);
			};
		}

		public async Task<Result<SiteDto>> Get(string siteId, string userId)
		{
			return await Validate(siteId, userId)
				.Bind(async () => await FindSite(siteId, userId))
				.Bind(Map)
				.OnFailure(LogError);

			Result<SiteDto> Map(Site entity) => Result.Success(mapper.Map<SiteDto>(entity));
		}

		public async Task<Result<SiteDto>> Update(SiteDto dto)
		{
			return await Map(dto)
				.Bind(Update)
				.OnFailure(LogError);

			Result<Site> Map(SiteDto site) => Result.Success(mapper.Map<Site>(site));

			async Task<Result<SiteDto>> Update(Site entity)
			{
				db.Sites.Update(entity);
				await db.SaveChangesAsync();

				return Result.Success(dto);
			};
		}

		public async Task<Result<bool>> Delete(string siteId, string userId)
		{
			return await Validate(siteId, userId)
				.Bind(async () => await FindSite(siteId, userId))
				.Bind(DeleteSiteFolder)
				.Bind(DeleteSite)
				.OnFailure(LogError);

			async Task<Result<Site>> DeleteSiteFolder(Site site)
			{
				var sitePath = siteGenerator.GetSitePath(siteId);
				siteGenerator.ClearSiteFolder(sitePath);

				return Result.Success(site);
			}

			async Task<Result<bool>> DeleteSite(Site site)
			{
				db.Sites.Remove(site);
				await db.SaveChangesAsync();

				return Result.Success(true);
			}
		}

		public async Task<Result<List<ProjectDto>>> GetProjects(string userId)
		{
			return await FindSites()
				.Bind(Map)
				.OnFailure(LogError);

			async Task<Result<List<Site>>> FindSites()
				=> Result.Success(await db.Sites
					.Where(x => x.UserId == userId)
					.OrderByDescending(x => x.UpdatedOn)
					.ToListAsync());

			Result<List<ProjectDto>> Map(List<Site> sites) => Result.Success(mapper.Map<List<ProjectDto>>(sites));
		}

		public async Task<Result<List<TemplateDto>>> GetTemplates()
		{
			return await GetTemplates()
				.Bind(Map)
				.OnFailure(LogError);

			async Task<Result<List<Template>>> GetTemplates() => Result.Success(await db.Templates.OrderBy(t => t.Name).ToListAsync());

			Result<List<TemplateDto>> Map(List<Template> templates) => Result.Success(mapper.Map<List<TemplateDto>>(templates));
		}

		public async Task<Result<List<TemplateKeyDto>>> GetTemplateKeys()
		{
			return await GetTemplateKeys()
				.Bind(Map)
				.OnFailure(LogError);

			async Task<Result<List<TemplateKey>>> GetTemplateKeys() => Result.Success(await db.TemplateKeys.OrderBy(t => t.Id).ToListAsync());

			Result<List<TemplateKeyDto>> Map(List<TemplateKey> keys) => Result.Success(mapper.Map<List<TemplateKeyDto>>(keys));
		}

		private async Task<Result<Site>> FindSite(string siteId, string userId)
		{
			var site = await db.Sites
				.Where(x => x.Id == siteId && x.UserId == userId)
				.Include(x => x.UserScripts)
				.Include(x => x.SectionNames)
				.FirstOrDefaultAsync() ?? Maybe<Site>.None;

			return site.HasNoValue
				? Result.Failure<Site>("Site not found")
				: Result.Success(site.Value);
		}

		private Result Validate(string siteId, string userId)
		{
			return string.IsNullOrEmpty(siteId) && string.IsNullOrEmpty(userId)
				? Result.Failure("Validation error, one or more fields are empty")
				: Result.Success();
		}
	}
}
