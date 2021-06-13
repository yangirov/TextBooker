using System.Collections.Generic;
using System.Threading.Tasks;

using CSharpFunctionalExtensions;

namespace TextBooker.BusinessLogic.Services
{
	public interface ISiteContentService<TEntity, TDto>
	{
		Task<Result<TDto>> Create(TDto dto);

		Task<Result<TDto>> Get(string id, string siteId);

		Task<Result<List<TDto>>> GetAll(string siteId);

		Task<Result<TDto>> Update(TDto dto);

		Task<Result<List<TDto>>> UpdateAll(List<TDto> list);

		Task<Result<bool>> Delete(string id, string siteId);
	}
}
