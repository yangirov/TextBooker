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
		/// Get all pages for site
		/// </summary>
		/// <param name="siteId">Site identifier</param>
		/// <returns></returns>
		[HttpGet("all")]
		public async Task<IActionResult> GetAllPages([FromQuery] string siteId)
			=> OkOrBadRequest(await pageService.GetAll(siteId));

		/// <summary>
	 	/// Create page
		/// </summary>
		/// <param name="dto">Page info data</param>
		/// <returns></returns>
		[HttpPost]
		public async Task<IActionResult> CreatePage([FromBody] PageDto dto)
			=> OkOrBadRequest(await pageService.Create(dto));

		/// <summary>
		/// Get page info
		/// </summary>
		/// <param name="id">Page identifier</param>
		/// <param name="siteId">Site identifier</param>
		/// <returns></returns>
		[HttpGet]
		public async Task<IActionResult> GetPage([FromQuery] string id, [FromQuery] string siteId)
			=> OkOrBadRequest(await pageService.Get(id, siteId));

		/// <summary>
		/// Update pages
		/// </summary>
		/// <param name="list">Pages data</param>
		/// <returns></returns>
		[HttpPut]
		public async Task<IActionResult> UpdatePage([FromBody] List<PageDto> list)
			=> OkOrBadRequest(await pageService.UpdateAll (list));

		/// <summary>
		/// Delete page
		/// </summary>
		/// <param name="id">Page identifier</param>
		/// <param name="siteId">Site identifier</param>
		/// <returns></returns>
		[HttpDelete]
		public async Task<IActionResult> DeletePage([FromQuery] string id, [FromQuery] string siteId)
			=> OkOrBadRequest(await pageService.Delete(id, siteId));
	}
}
