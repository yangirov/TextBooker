using System.Threading.Tasks;

using CSharpFunctionalExtensions;

namespace TextBooker.BusinessLogic.Services
{
	public interface ISiteContentService<T>
	{
		Task<Result<T>> Create(T dto);

		Task<Result<T>> Get(string id, string userId);

		Task<Result<T>> Update(T dto);

		Task<Result<T>> Delete(string id, string userId);
	}
}
