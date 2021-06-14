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
		private readonly ISiteSettingsService siteService;
		private readonly ISiteGenerator siteGenerator;

		public EditorController(ISiteSettingsService siteService, ISiteGenerator siteGenerator)
		{
			this.siteService = siteService;
			this.siteGenerator = siteGenerator;
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

		/// <summary>
		/// Generate site
		/// </summary>
		/// <param name="siteId">Site identifier</param>
		/// <returns></returns>
		[HttpGet("generate")]
		public async Task<IActionResult> GenerateSite(string siteId) => OkOrBadRequest(await siteGenerator.Generate(siteId, UserId));
	}
}
