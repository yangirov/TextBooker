using System;
using System.Threading.Tasks;

using AutoMapper;

using CSharpFunctionalExtensions;

using Serilog;

using TextBooker.Contracts.Dto;
using TextBooker.DataAccess;

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

		public Task<Result<BlockDto>> Create(BlockDto dto)
		{
			throw new NotImplementedException();
		}

		public Task<Result<BlockDto>> Update(BlockDto dto)
		{
			throw new NotImplementedException();
		}

		public Task<Result<BlockDto>> Delete(string id, string userId)
		{
			throw new NotImplementedException();
		}

		public Task<Result<BlockDto>> Get(string id, string userId)
		{
			throw new NotImplementedException();
		}
	}
}
