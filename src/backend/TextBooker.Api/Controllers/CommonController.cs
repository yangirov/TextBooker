using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using TextBooker.BusinessLogic.Services;
using TextBooker.Contracts.Dto;

namespace TextBooker.Api.Controllers
{
	[AllowAnonymous]
	[ApiController]
	[Produces("application/json")]
	public class CommonController : BaseController
	{
		private readonly ICommonService commonService;

		public CommonController(ICommonService commonService)
		{
			this.commonService = commonService;
		}

		/// <summary>
		/// Get system settings
		/// </summary>
		/// <returns></returns>
		[HttpGet("settings")]
		public IActionResult GetSetting() => OkOrBadRequest(commonService.GetSettings());

		/// <summary>
		/// Send feedback
		/// </summary>
		/// <param name="dto">Feedback message</param>
		/// <returns></returns>
		[HttpPost("feedback")]
		public async Task<IActionResult> SendFeedback(Feedback dto) => OkOrBadRequest(await commonService.SendFeedback(dto));
	}
}
