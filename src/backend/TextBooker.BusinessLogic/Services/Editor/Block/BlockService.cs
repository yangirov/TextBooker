using System;
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
	public class BlockService : BaseService, IBlockService
	{
		private IMapper mapper;
		private readonly TextBookerContext db;

		public BlockService(
			IMapper mapper,
			ILogger logger,
			TextBookerContext db
		) : base(logger, db)
		{
			this.mapper = mapper;
			this.db = db;
		}

		public async Task<Result<List<BlockDto>>> UpdateAll(List<BlockDto> list)
		{
			return await MapDto(list)
				.Bind(Update)
				.Bind(MapEntity)
				.OnFailure(LogError);

			Result<List<Block>> MapDto(List<BlockDto> list) => Result.Ok(mapper.Map<List<Block>>(list));

			async Task<Result<List<Block>>> Update(List<Block> list)
			{
				db.Blocks.UpdateRange(list);
				await db.SaveChangesAsync();

				return Result.Ok(list);
			};

			Result<List<BlockDto>> MapEntity(List<Block> list) => Result.Ok(mapper.Map<List<BlockDto>>(list));
		}

		public async Task<Result<List<BlockDto>>> GetAll(string siteId)
		{
			return await FindBlocks(siteId)
				.Bind(MapList)
				.OnFailure(LogError);

			async Task<Result<List<Block>>> FindBlocks(string siteId)
			{
				var blocks = await db.Blocks
					.Where(x => x.SiteId == siteId)
						.Include(s => s.Site)
					.ToListAsync();

				return Result.Ok(blocks);
			}

			Result<List<BlockDto>> MapList(List<Block> list) => Result.Ok(mapper.Map<List<BlockDto>>(list));
		}

		public async Task<Result<BlockDto>> Create(BlockDto dto)
		{
			return await MapDto(dto)
				.Bind(Create)
				.Bind(MapEntity)
				.OnFailure(LogError);

			async Task<Result<Block>> Create(Block entity)
			{
				db.Blocks.Add(entity);
				await db.SaveChangesAsync();

				return Result.Ok(entity);
			};
		}

		public async Task<Result<BlockDto>> Get(string id, string siteId)
		{
			return await FindBlock(id, siteId)
				.Bind(MapEntity)
				.OnFailure(LogError);
		}

		public async Task<Result<BlockDto>> Update(BlockDto dto)
		{
			return await MapDto(dto)
				.Bind(Update)
				.Bind(MapEntity)
				.OnFailure(LogError);

			async Task<Result<Block>> Update(Block entity)
			{
				db.Blocks.Update(entity);
				await db.SaveChangesAsync();

				return Result.Ok(entity);
			};
		}

		public async Task<Result<bool>> Delete(string id, string siteId)
		{
			return await FindBlock(id, siteId)
				.Bind(Delete)
				.OnFailure(LogError);

			async Task<Result<bool>> Delete(Block entity)
			{
				db.Blocks.Remove(entity);
				await db.SaveChangesAsync();

				return Result.Ok(true);
			};
		}

		private async Task<Result<Block>> FindBlock(string id, string siteId)
		{
			var block = await db.Blocks
				.Where(x => x.Id == id && x.SiteId == siteId)
					.Include(s => s.Site)
				.FirstOrDefaultAsync() ?? Maybe<Block>.None;

			return block.HasNoValue
				? Result.Failure<Block>("Block not found")
				: Result.Ok(block.Value);
		}

		private Result<Block> MapDto(BlockDto dto) => Result.Ok(mapper.Map<Block>(dto));

		private Result<BlockDto> MapEntity(Block entity) => Result.Ok(mapper.Map<BlockDto>(entity));
	}
}
