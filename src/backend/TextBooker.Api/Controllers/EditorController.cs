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
		public async Task<IActionResult> CreateSite([FromBody] SiteDto dto)
		{
			dto.UserId = UserId;
			return OkOrBadRequest(await editorService.Create(dto));
		}

		/// <summary>
		/// Get site info
		/// </summary>
		/// <param name="id">Site identifier</param>
		/// <returns></returns>
		[HttpGet("site")]
		public async Task<IActionResult> GetSite([FromQuery] string id) => OkOrBadRequest(await editorService.Get(id, UserId));

		/// <summary>
		/// Get user projects (sites) list
		/// </summary>
		/// <returns></returns>
		[HttpGet("projects")]
		public async Task<IActionResult> GetUserSites() => OkOrBadRequest(await editorService.GetUserSites(UserId));

		/// <summary>
		/// Get templates list
		/// </summary>
		/// <returns></returns>
		[HttpGet("templates")]
		public async Task<IActionResult> GetTemplates() => OkOrBadRequest(await editorService.GetTemplates());

		/// <summary>
		/// Get template section names
		/// </summary>
		/// <returns></returns>
		[HttpGet("template-keys")]
		public async Task<IActionResult> GetTemplateKeys() => OkOrBadRequest(await editorService.GetTemplateKeys());
	}
}
