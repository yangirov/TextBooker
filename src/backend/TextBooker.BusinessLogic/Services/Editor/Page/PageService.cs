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
	public class PageService : BaseService, IPageService
	{
		private IMapper mapper;
		private readonly TextBookerContext db;

		public PageService(
			IMapper mapper,
			ILogger logger,
			TextBookerContext db
		) : base(logger, db)
		{
			this.mapper = mapper;
			this.db = db;
		}

		public async Task<Result<List<PageDto>>> GetAll(string siteId)
		{
			return await FindPages(siteId)
				.Bind(MapList)
				.OnFailure(LogError);

			async Task<Result<List<Page>>> FindPages(string siteId)
			{
				var pages = await db.Pages
					.Where(x => x.Site.Id == siteId)
						.Include(s => s.Site)
					.ToListAsync();

				return Result.Ok(pages);
			}

			Result<List<PageDto>> MapList(List<Page> list) => Result.Ok(mapper.Map<List<PageDto>>(list));
		}

		public async Task<Result<PageDto>> Create(PageDto dto)
		{
			return await MapDto(dto)
				.Bind(Create)
				.Bind(MapEntity)
				.OnFailure(LogError);

			async Task<Result<Page>> Create(Page entity)
			{
				db.Pages.Add(entity);
				await db.SaveChangesAsync();

				return Result.Ok(entity);
			};
		}

		public async Task<Result<PageDto>> Get(string id, string siteId)
		{
			return await FindPage(id, siteId)
				.Bind(MapEntity)
				.OnFailure(LogError);
		}

		public async Task<Result<PageDto>> Update(PageDto dto)
		{
			return await MapDto(dto)
				.Bind(Update)
				.Bind(MapEntity)
				.OnFailure(LogError);

			async Task<Result<Page>> Update(Page entity)
			{
				db.Pages.Update(entity);
				await db.SaveChangesAsync();

				return Result.Ok(entity);
			};
		}

		public async Task<Result<bool>> Delete(string id, string siteId)
		{
			return await FindPage(id, siteId)
				.Bind(Delete)
				.OnFailure(LogError);

			async Task<Result<bool>> Delete(Page entity)
			{
				db.Pages.Remove(entity);
				await db.SaveChangesAsync();

				return Result.Ok(true);
			};
		}

		private async Task<Result<Page>> FindPage(string id, string siteId)
		{
			var page = await db.Pages
				.Where(x => x.Id == id && x.Site.Id == siteId)
					.Include(s => s.Site)
				.FirstOrDefaultAsync() ?? Maybe<Page>.None;

			return page.HasNoValue
				? Result.Failure<Page>("Page not found")
				: Result.Ok(page.Value);
		}

		private Result<Page> MapDto(PageDto dto) => Result.Ok(mapper.Map<Page>(dto));

		private Result<PageDto> MapEntity(Page entity) => Result.Ok(mapper.Map<PageDto>(entity));
	}
}
