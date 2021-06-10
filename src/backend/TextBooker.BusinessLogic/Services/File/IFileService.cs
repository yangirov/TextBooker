using System.Threading.Tasks;

using CSharpFunctionalExtensions;

using TextBooker.Contracts.Dto;

namespace TextBooker.BusinessLogic.Services
{
	public interface IFileService
	{
		Task<Result<string>> Upload(FileUploadDto dto);
	}
}
