using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using TextBooker.BusinessLogic.Services;
using TextBooker.Contracts.Dto;

namespace TextBooker.Api.Controllers
{
	[Authorize]
	[ApiController]
	[Route("file")]
	[Produces("application/json")]
	public class FilesController : BaseController
	{
		private readonly IFileService filesService;

		public FilesController(IFileService filesService)
		{
			this.filesService = filesService;
		}

		/// <summary>
		/// Upload file
		/// Max size: 2 Mb
		/// </summary>
		/// <returns>URL</returns>
		[RequestSizeLimit(200_000_000)]
		[HttpPost("upload")]
		public async Task<IActionResult> SendFeedback([FromForm] FileUploadDto dto)
		{
			dto.UserId = UserId;
			return OkOrBadRequest(await filesService.Upload(dto));
		}
	}
}
