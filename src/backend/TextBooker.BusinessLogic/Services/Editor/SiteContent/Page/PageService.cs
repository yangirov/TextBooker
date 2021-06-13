using AutoMapper;

using Serilog;

using TextBooker.Contracts.Dto;
using TextBooker.DataAccess;
using TextBooker.DataAccess.Entities;

namespace TextBooker.BusinessLogic.Services
{
	public class PageService : SiteContentService<Page, PageDto>, IPageService
	{
		public PageService(IMapper mapper, ILogger logger, TextBookerContext db) : base(mapper, logger, db, "Page")
		{
		}
	}
}
