using Ecommerce.Shared.DTOs.Authors;
using Ecommerce.Shared;

namespace Ecommerce.Server.Services.BookTypeService
{
	public interface IBookTypeService
	{
		Task<ServiceResponse<List<DataSelectDTO>>> GetBookTypes();

	}
}
