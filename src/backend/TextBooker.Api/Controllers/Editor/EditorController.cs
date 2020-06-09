using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using TextBooker.BusinessLogic.Services;

namespace TextBooker.Api.Controllers
{
	[Authorize]
	[ApiController]
	[Route("editor")]
	[Produces("application/json")]
	public class EditorController : BaseController
	{
		private readonly ISiteService siteService;

		public EditorController(ISiteService siteService)
		{
			this.siteService = siteService;
			this.siteService = siteService;
		}

        /// <summary>
        /// Get user projects list
        /// </summary>
        /// <returns></returns>
        [HttpGet("projects")]
        public async Task<IActionResult> GetProjects() => OkOrBadRequest(await siteService.GetProjects(UserId));

        /// <summary>
        /// Get templates list
        /// </summary>
        /// <returns></returns>
        [HttpGet("templates")]
        public async Task<IActionResult> GetTemplates() => OkOrBadRequest(await siteService.GetTemplates());

        /// <summary>
        /// Get template sections
        /// </summary>
        /// <returns></returns>
        [HttpGet("template-keys")]
        public async Task<IActionResult> GetTemplateKeys() => OkOrBadRequest(await siteService.GetTemplateKeys());
    }
}
