using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

using AutoMapper;

using CSharpFunctionalExtensions;

using Microsoft.AspNetCore.Http;

using Serilog;
using TextBooker.Common;
using TextBooker.Common.Config;
using TextBooker.Contracts.Dto;
using TextBooker.DataAccess;
using TextBooker.DataAccess.Entities;

namespace TextBooker.BusinessLogic.Services
{
	public class FileService : BaseService, IFileService
	{
		private readonly IMapper mapper;
		private readonly TextBookerContext db;
		private readonly FileStoreSettings fileStoreSettings;

		public FileService(
			IMapper mapper,
			ILogger logger,
			TextBookerContext db,
			FileStoreSettings fileStoreSettings
		) : base(logger)
        {
			this.mapper = mapper;
			this.db = db;
			this.fileStoreSettings = fileStoreSettings;
		}

		public async Task<Result<string>> Upload(FileUploadDto dto)
		{
			var fileExists = CheckFileExists(dto);
			if (fileExists.HasValue)
				return Result.Success(fileExists.Value);

			return await FillDto(dto)
				.Bind(UploadFile)
				.Bind(Map)
				.Bind(SaveFileInfo)
				.OnFailure(LogError);
		}

		private Maybe<string> CheckFileExists(FileUploadDto dto)
		{
			var filePath = StringUtils.GetFilePath(dto.SiteId, dto.File.FileName);
			var fullFilePath = Path.Combine(fileStoreSettings.BasePath, filePath);

			return File.Exists(fullFilePath)
				? StringUtils.ConvertToUrl(filePath)
				: Maybe<string>.None;
		}

		private Result<FileUploadDto> FillDto(FileUploadDto dto)
		{
			dto.Hash = GetMD5Hash(dto.File);
			dto.FileName = dto.File.FileName;
			dto.Length = dto.File.Length;
			dto.FilePath = StringUtils.GetFilePath(dto.SiteId, dto.FileName);

			return Result.Success(dto);
		}

		private async Task<Result<FileUploadDto>> UploadFile(FileUploadDto dto)
		{
			try
			{
				// TODO: use custom storage, like https://api.imgbb.com/
				var filePath = Path.Combine(fileStoreSettings.BasePath, dto.FilePath);
				var directoryPath = Path.GetDirectoryName(filePath);

				if (!Directory.Exists(directoryPath))
					Directory.CreateDirectory(directoryPath);

				await using var fileStream = new FileStream(filePath, FileMode.Create);
				await dto.File.CopyToAsync(fileStream);

				return Result.Success(dto);
			}
			catch (Exception)
			{
				return Result.Failure<FileUploadDto>("An error occurred while uploading the file");
			}
		}

		private Result<SiteFile> Map(FileUploadDto dto) => Result.Success(mapper.Map<SiteFile>(dto));

		private async Task<Result<string>> SaveFileInfo(SiteFile entity)
		{
			db.Files.Add(entity);
			await db.SaveChangesAsync();

			var fileUrl = StringUtils.ConvertToUrl(entity.FilePath);
			return Result.Success(fileUrl);
		}

		private static string GetMD5Hash(IFormFile file)
		{
			var stream = file.OpenReadStream();
			var mst = new MemoryStream();
			stream.CopyTo(mst);

			return ToMD5Hash(mst.ToArray());

			static string ToMD5Hash(byte[] bytes)
			{
				if (bytes == null || bytes.Length == 0)
					return null;

				using var md5 = MD5.Create();
				return string.Join("", md5.ComputeHash(bytes).Select(x => x.ToString("X2")));
			}
		}
	}
}
