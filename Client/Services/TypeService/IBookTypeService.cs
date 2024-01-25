using Ecommerce.Shared;
using Ecommerce.Shared.DTOs.Authors;

namespace Ecommerce.Client.Services.TypeService
{
	public interface IBookTypeService
	{

		event Action? OnChange;

		Task<ServiceResponse<List<DataSelectDTO>>> GetBookTypes();
	}
}
