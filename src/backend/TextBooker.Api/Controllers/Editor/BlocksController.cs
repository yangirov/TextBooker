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
        /// Create block
        /// </summary>
        /// <param name="dto">Block info data</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateBlock([FromBody] BlockDto dto) => OkOrBadRequest(await blockService.Create(dto));

		/// <summary>
		/// Get block info
		/// </summary>
		/// <param name="id">Block identifier</param>
		/// <returns></returns>
		[HttpGet]
		public async Task<IActionResult> GetBlock([FromQuery] string id) => OkOrBadRequest(await blockService.Get(id, UserId));

		/// <summary>
		/// Update block info
		/// </summary>
		/// <param name="dto">Block info data</param>
		/// <returns></returns>
		[HttpPut]
        public async Task<IActionResult> UpdateBlock([FromBody] BlockDto dto) => OkOrBadRequest(await blockService.Update(dto));

        /// <summary>
        /// Delete block
        /// </summary>
        /// <param name="id">Block identifier</param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteBlock([FromQuery] string id) => OkOrBadRequest(await blockService.Delete(id, UserId));
    }
}
