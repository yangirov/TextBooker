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
			return await Map(dto)
				.Bind(Create)
				.OnFailure(LogError);

			Result<Site> Map(SiteCreateDto site) => Result.Ok(mapper.Map<Site>(site));

			async Task<Result<SiteCreateDto>> Create(Site entity)
			{
				db.Sites.Add(entity);
				await db.SaveChangesAsync();

				return Result.Ok(dto);
			};
		}

		public async Task<Result<List<SiteListItemDto>>> GetUserSites(string userId)
		{
			return await FindSites()
				.Bind(Map)
				.OnFailure(LogError);

			async Task<Result<List<Site>>> FindSites()
				=> Result.Ok(
					await db.Sites
							.Where(x => x.UserId == userId)
							.OrderByDescending(x => x.UpdatedOn)
							.ToListAsync());

			Result<List<SiteListItemDto>> Map(List<Site> sites)
				=> Result.Ok(mapper.Map<List<SiteListItemDto>>(sites));
		}

		public async Task<Result<List<TemplateDto>>> GetTemplates()
		{
			return await FindTemplates()
				.Bind(Map)
				.OnFailure(LogError);

			async Task<Result<List<Template>>> FindTemplates()
				=> Result.Ok(await db.Templates.OrderBy(t => t.Name).ToListAsync());

			Result<List<TemplateDto>> Map(List<Template> templates)
				=> Result.Ok(mapper.Map<List<TemplateDto>>(templates));
		}
	}
}
