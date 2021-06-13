using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using TextBooker.BusinessLogic.Services;
using TextBooker.Contracts.Dto;

namespace TextBooker.Api.Controllers
{
	[Authorize]
	[ApiController]
	[Route("editor/site")]
	[Produces("application/json")]
	public class SiteSettingsController : BaseController
	{
		private readonly ISiteSettingsService siteService;

		public SiteSettingsController(ISiteSettingsService siteService)
		{
			this.siteService = siteService;
		}

		/// <summary>
		/// Create site
		/// </summary>
		/// <param name="dto">Site data</param>
		/// <returns></returns>
		[HttpPost]
		public async Task<IActionResult> CreateSite([FromBody] SiteDto dto)
		{
			dto.UserId = UserId;
			return OkOrBadRequest(await siteService.Create(dto));
		}

		/// <summary>
		/// Get site info
		/// </summary>
		/// <param name="id">Site identifier</param>
		/// <returns></returns>
		[HttpGet]
		public async Task<IActionResult> GetSite([FromQuery] string id) => OkOrBadRequest(await siteService.Get(id, UserId));

		/// <summary>
		/// Update site info
		/// </summary>
		/// <param name="dto">Site data</param>
		/// <returns></returns>
		[HttpPut]
		public async Task<IActionResult> UpdateSite([FromBody] SiteDto dto)
		{
			dto.UserId = UserId;
			return OkOrBadRequest(await siteService.Update(dto));
		}

		/// <summary>
		/// Delete site
		/// </summary>
		/// <param name="id">Site identifier</param>
		/// <returns></returns>
		[HttpDelete]
		public async Task<IActionResult> DeleteSite([FromQuery] string id) => OkOrBadRequest(await siteService.Delete(id, UserId));
	}
}
