using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using TextBooker.BusinessLogic.Services;
using TextBooker.Contracts.Dto;

namespace TextBooker.Api.Controllers
{
	[Authorize]
	[ApiController]
	[Route("editor/block")]
	[Produces("application/json")]
	public class BlocksController : BaseController
	{
		private readonly IBlockService blockService;

		public BlocksController(IBlockService blockService)
		{
			this.blockService = blockService;
		}

		/// <summary>
		/// Get all blocks for site
		/// </summary>
		/// <param name="siteId">Site identifier</param>
		/// <returns></returns>
		[HttpGet("all")]
		public async Task<IActionResult> GetAllBlocks([FromQuery] string siteId)
			=> OkOrBadRequest(await blockService.GetAll(siteId));

		/// <summary>
		/// Create block
		/// </summary>
		/// <param name="dto">Block info data</param>
		/// <returns></returns>
		[HttpPost]
        public async Task<IActionResult> CreateBlock([FromBody] BlockDto dto)
			=> OkOrBadRequest(await blockService.Create(dto));

		/// <summary>
		/// Get block info
		/// </summary>
		/// <param name="id">Block identifier</param>
		/// <param name="siteId">Site identifier</param>
		/// <returns></returns>
		[HttpGet]
		public async Task<IActionResult> GetBlock([FromQuery] string id, [FromQuery] string siteId)
			=> OkOrBadRequest(await blockService.Get(id, siteId));

		/// <summary>
		/// Update blocks
		/// </summary>
		/// <param name="list">Blocks data</param>
		/// <returns></returns>
		[HttpPut]
        public async Task<IActionResult> UpdateBlocks([FromBody] List<BlockDto> list)
			=> OkOrBadRequest(await blockService.UpdateAll(list));

		/// <summary>
		/// Delete block
		/// </summary>
		/// <param name="id">Block identifier</param>
		/// <param name="siteId">Site identifier</param>
		/// <returns></returns>
		[HttpDelete]
        public async Task<IActionResult> DeleteBlock([FromQuery] string id, [FromQuery] string siteId)
			=> OkOrBadRequest(await blockService.Delete(id, siteId));
    }
}
