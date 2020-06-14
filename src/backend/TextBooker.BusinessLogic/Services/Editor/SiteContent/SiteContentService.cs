using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using CSharpFunctionalExtensions;

using Microsoft.EntityFrameworkCore;

using Serilog;

using TextBooker.DataAccess;
using TextBooker.DataAccess.Entities;

namespace TextBooker.BusinessLogic.Services
{
	public class SiteContentService<TEntity, TDto> : BaseService, ISiteContentService<TEntity, TDto> where TEntity : SiteContent
	{
		private IMapper mapper;
		private readonly TextBookerContext db;
		private readonly string entityName;

		public SiteContentService(
			IMapper mapper,
			ILogger logger,
			TextBookerContext db,
			string entityName
		) : base(logger, db)
		{
			this.mapper = mapper;
			this.db = db;
			this.entityName = entityName;
		}

		public async Task<Result<TDto>> Create(TDto dto)
		{
			return await MapDto(dto)
				.Bind(Create)
				.Bind(MapEntity)
				.OnFailure(LogError);

			async Task<Result<TEntity>> Create(TEntity entity)
			{
				db.Set<TEntity>().Add(entity);
				await db.SaveChangesAsync();

				return Result.Ok(entity);
			};
		}

		public async Task<Result<TDto>> Get(string id, string siteId)
		{
			return await Find(id, siteId)
				.Bind(MapEntity)
				.OnFailure(LogError);
		}

		public async Task<Result<List<TDto>>> GetAll(string siteId)
		{
			return await Finds(siteId)
				.Bind(MapList)
				.OnFailure(LogError);

			async Task<Result<List<TEntity>>> Finds(string siteId)
			{
				var Ts = await db.Set<TEntity>()
					.Where(x => x.SiteId == siteId)
						.Include(s => s.Site)
					.OrderBy(x => x.Order)
					.ToListAsync();

				return Result.Ok(Ts);
			}

			Result<List<TDto>> MapList(List<TEntity> list) => Result.Ok(mapper.Map<List<TDto>>(list));
		}

		public async Task<Result<TDto>> Update(TDto dto)
		{
			return await MapDto(dto)
				.Bind(Update)
				.Bind(MapEntity)
				.OnFailure(LogError);

			async Task<Result<TEntity>> Update(TEntity entity)
			{
				db.Set<TEntity>().Update(entity);
				await db.SaveChangesAsync();

				return Result.Ok(entity);
			};
		}

		public async Task<Result<List<TDto>>> UpdateAll(List<TDto> list)
		{
			return await MapDto(list)
				.Bind(Update)
				.Bind(MapEntity)
				.OnFailure(LogError);

			Result<List<TEntity>> MapDto(List<TDto> list) => Result.Ok(mapper.Map<List<TEntity>>(list));

			async Task<Result<List<TEntity>>> Update(List<TEntity> list)
			{
				db.Set<TEntity>().UpdateRange(list);
				await db.SaveChangesAsync();

				return Result.Ok(list);
			};

			Result<List<TDto>> MapEntity(List<TEntity> list) => Result.Ok(mapper.Map<List<TDto>>(list));
		}

		public async Task<Result<bool>> Delete(string id, string siteId)
		{
			return await Find(id, siteId)
				.Bind(Delete)
				.OnFailure(LogError);

			async Task<Result<bool>> Delete(TEntity entity)
			{
				db.Set<TEntity>().Remove(entity);
				await db.SaveChangesAsync();

				return Result.Ok(true);
			};
		}

		private async Task<Result<TEntity>> Find(string id, string siteId)
		{
			var T = await db.Set<TEntity>()
				.Where(x => x.Id == id && x.SiteId == siteId)
					.Include(s => s.Site)
				.FirstOrDefaultAsync() ?? Maybe<TEntity>.None;

			return T.HasNoValue
				? Result.Failure<TEntity>($"{entityName} not found")
				: Result.Ok(T.Value);
		}

		private Result<TEntity> MapDto(TDto dto) => Result.Ok(mapper.Map<TEntity>(dto));

		private Result<TDto> MapEntity(TEntity entity) => Result.Ok(mapper.Map<TDto>(entity));
	}
}
