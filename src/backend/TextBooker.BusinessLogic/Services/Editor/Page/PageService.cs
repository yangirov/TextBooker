using System;
using System.Threading.Tasks;

using AutoMapper;

using CSharpFunctionalExtensions;

using Serilog;

using TextBooker.Contracts.Dto;
using TextBooker.DataAccess;

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

		public Task<Result<PageDto>> Create(PageDto dto)
		{
			throw new NotImplementedException();
		}

		public Task<Result<PageDto>> Update(PageDto dto)
		{
			throw new NotImplementedException();
		}

		public Task<Result<PageDto>> Delete(string id, string userId)
		{
			throw new NotImplementedException();
		}

		public Task<Result<PageDto>> Get(string id, string userId)
		{
			throw new NotImplementedException();
		}
	}
}
