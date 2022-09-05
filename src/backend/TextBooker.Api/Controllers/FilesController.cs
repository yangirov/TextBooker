using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using TextBooker.BusinessLogic.Services;
using TextBooker.Common.Config;
using TextBooker.Contracts.Dto;

namespace TextBooker.Api.Controllers
{
	[Authorize]
	[ApiController]
	[Route("file")]
	[Produces("application/json")]
	[ApiExplorerSettings(IgnoreApi = true)]
	public class FilesController : BaseController
	{
		private readonly IFileService filesService;
		private readonly FileStoreSettings fileStoreSettings;

		public FilesController(IFileService filesService, FileStoreSettings fileStoreSettings)
		{
			this.filesService = filesService;
			this.fileStoreSettings = fileStoreSettings;
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

		[HttpGet("download/{siteId}")]
		public async Task DownloadSite(string siteId)
		{
			Response.ContentType = "application/octet-stream";
			Response.Headers.Add("Content-Disposition", $"attachment; filename=\"{siteId}.zip\"");

			var siteFolderPath = GetSitePath(siteId);
			if (!Directory.Exists(siteFolderPath))
				Directory.CreateDirectory(siteFolderPath);

			var files = Directory.GetFiles(siteFolderPath, "*.*", SearchOption.AllDirectories);

			using var archive = new ZipArchive(Response.BodyWriter.AsStream(), ZipArchiveMode.Create);
			foreach (var filePath in files)
			{
				var entryPath = filePath.Replace(GetSitePath(siteId), "");
				archive.CreateEntryFromFile(filePath, entryPath, CompressionLevel.Fastest);
			}

			string GetSitePath(string siteId) => Path.Combine(fileStoreSettings.BasePath, "sites", siteId);
		}
	}
}

