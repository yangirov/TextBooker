using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Serilog;

using TextBooker.Common.Enums;
using TextBooker.Contracts.Dto;
using TextBooker.DataAccess;
using TextBooker.DataAccess.Entities;

namespace TextBooker.BusinessLogic.Services
{
	public class EditorService : BaseService, IEditorService
	{
		private readonly IMapper mapper;
		private readonly TextBookerContext db;

		public EditorService(
			IMapper mapper,
			ILogger logger,
			TextBookerContext db
		) : base(logger, db)
		{
			this.mapper = mapper;
			this.db = db;
		}

		public async Task<Result<SiteCreateDto>> Create(SiteCreateDto dto)
		{
			var entity = mapper.Map<Site>(dto);

			db.Sites.Add(entity);
			await db.SaveChangesAsync();

			return Result.Ok(new SiteCreateDto());
		}

		public async Task<Result<List<SiteListItemDto>>> GetUserSites(string userId)
		{
			return await FindSites()
				.Bind(list => Map(list))
				.OnFailure(LogError);

			async Task<Result<List<Site>>> FindSites()
				=> Result.Ok(await db.Sites.Where(x => x.UserId == userId).ToListAsync());

			Result<List<SiteListItemDto>> Map(List<Site> sites)
				=> Result.Ok(mapper.Map<List<SiteListItemDto>>(sites));
		}
	}
}
