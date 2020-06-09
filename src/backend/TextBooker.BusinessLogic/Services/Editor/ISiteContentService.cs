using System.Collections.Generic;
using System.Threading.Tasks;

using CSharpFunctionalExtensions;

namespace TextBooker.BusinessLogic.Services
{
	public interface ISiteContentService<T>
	{
		Task<Result<List<T>>> GetAll(string siteId);

		Task<Result<T>> Create(T dto);

		Task<Result<T>> Get(string id, string siteId);

		Task<Result<T>> Update(T dto);

		Task<Result<bool>> Delete(string id, string siteId);
	}
}
