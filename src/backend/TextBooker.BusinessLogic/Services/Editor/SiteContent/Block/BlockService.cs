using AutoMapper;

using Serilog;

using TextBooker.Contracts.Dto;
using TextBooker.DataAccess;
using TextBooker.DataAccess.Entities;

namespace TextBooker.BusinessLogic.Services
{
	public class BlockService : SiteContentService<Block, BlockDto>, IBlockService
	{
		public BlockService(IMapper mapper, ILogger logger, TextBookerContext db) : base(mapper, logger, db, "Block")
		{
		}
	}
}
