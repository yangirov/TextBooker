using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using TextBooker.BusinessLogic.Services;
using TextBooker.Contracts.Dto;

namespace TextBooker.Api.Controllers
{
	[Authorize]
	[ApiController]
	[Route("editor/page")]
	[Produces("application/json")]
	public class PagesController : BaseController
	{
		private readonly IPageService pageService;

		public PagesController(IPageService pageService)
		{
			this.pageService = pageService;
		}

        /// <summary>
        /// Create page
        /// </summary>
        /// <param name="dto">Page info data</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreatePage([FromBody] PageDto dto) => OkOrBadRequest(await pageService.Create(dto));

		/// <summary>
		/// Get page info
		/// </summary>
		/// <param name="id">Page identifier</param>
		/// <returns></returns>
		[HttpGet]
		public async Task<IActionResult> GetPage([FromQuery] string id) => OkOrBadRequest(await pageService.Get(id, UserId));

		/// <summary>
		/// Update page info
		/// </summary>
		/// <param name="dto">Page info data</param>
		/// <returns></returns>
		[HttpPut]
        public async Task<IActionResult> UpdatePage([FromBody] PageDto dto) => OkOrBadRequest(await pageService.Update(dto));

        /// <summary>
        /// Delete page
        /// </summary>
        /// <param name="id">Page identifier</param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> DeletePage([FromQuery] string id) => OkOrBadRequest(await pageService.Delete(id, UserId)); 
    }
}
