using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TextBooker.BusinessLogic.Services;
using TextBooker.Contracts.Dto;

namespace TextBooker.Api.Controllers
{
	[Authorize]
	[ApiController]
	[Route("editor")]
	[Produces("application/json")]
	public class EditorController : BaseController
	{
		private readonly IEditorService editorService;

		public EditorController(IEditorService editorService)
		{
			this.editorService = editorService;
		}

		/// <summary>
		/// Create site with initial info
		/// </summary>
		/// <param name="dto">Site short info data</param>
		/// <returns></returns>
		[HttpPost("site")]
		public async Task<IActionResult> CreateSite([FromBody] SiteCreateDto dto)
		{
			dto.UserId = UserId;
			return OkOrBadRequest(await editorService.Create(dto));
		}

		[HttpGet("site")]
		public async Task<IActionResult> GetUserSites() => OkOrBadRequest(await editorService.GetUserSites(UserId));
	}
}
