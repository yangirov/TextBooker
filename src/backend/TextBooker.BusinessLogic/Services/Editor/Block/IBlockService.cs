using System.Collections.Generic;
using System.Threading.Tasks;

using CSharpFunctionalExtensions;

using TextBooker.Contracts.Dto;

namespace TextBooker.BusinessLogic.Services
{
	public interface IBlockService : ISiteContentService<BlockDto>
	{
		Task<Result<List<BlockDto>>> UpdateAll(List<BlockDto> list);
	}
}
