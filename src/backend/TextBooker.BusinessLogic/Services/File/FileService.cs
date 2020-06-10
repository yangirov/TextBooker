using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

using AutoMapper;

using CSharpFunctionalExtensions;

using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

using Serilog;

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
		) : base(logger, db)
		{
			this.mapper = mapper;
			this.db = db;
			this.fileStoreSettings = fileStoreSettings;
		}

		public async Task<Result<string>> Upload(FileUploadDto dto)
		{
			return await FillDto(dto)
				.Bind(UploadFile)
				.Bind(Map)
				.Bind(SaveFileInfo)
				.OnFailure(LogError);

			static Result<FileUploadDto> FillDto(FileUploadDto dto)
			{
				dto.Hash = GetMD5Hash(dto.File);
				dto.FileName = dto.File.FileName;
				dto.Length = dto.File.Length;
				dto.FilePath = Path.Combine(dto.SiteId, "assets", dto.FileName);

				return Result.Ok(dto);
			}

			async Task<Result<FileUploadDto>> UploadFile(FileUploadDto dto)
			{
				try
				{
					var filePath = Path.Combine(fileStoreSettings.BasePath, dto.FilePath);
					var directoryPath = Path.GetDirectoryName(filePath);

					if (!Directory.Exists(directoryPath))
						Directory.CreateDirectory(directoryPath);

					if (File.Exists(filePath))
						return Result.Ok(dto);

					using var fileStream = new FileStream(filePath, FileMode.Create);
					await dto.File.CopyToAsync(fileStream);

					return Result.Ok(dto);
				}
				catch (Exception ex)
				{
					return Result.Failure<FileUploadDto>("An error occurred while uploading the file");
				}
			}

			Result<SiteFile> Map(FileUploadDto dto) => Result.Ok(mapper.Map<SiteFile>(dto));

			async Task<Result<string>> SaveFileInfo(SiteFile entity)
			{
				var fileExists = await db.Files
					.Where(x => x.SiteId == entity.SiteId && x.FileName == entity.FileName)
					.FirstOrDefaultAsync() ?? Maybe<SiteFile>.None;

				if (fileExists.HasNoValue)
				{
					db.Files.Add(entity);
					await db.SaveChangesAsync();
				}

				return Result.Ok(entity.FilePath);
			}
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