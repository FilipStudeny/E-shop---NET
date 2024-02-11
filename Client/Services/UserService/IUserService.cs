using Ecommerce.Shared;
using Ecommerce.Shared.DTOs;
using Ecommerce.Shared.DTOs.Authors;
using Ecommerce.Shared.DTOs.Books;
using Ecommerce.Shared.DTOs.Series;
using Ecommerce.Shared.DTOs.Users;
using Ecommerce.Shared.User;

namespace Ecommerce.Client.Services.UserService
{
    public interface IUserService
    {
		List<UserDTO> Users { get; set; }
		event Action? OnChange;

		int UsersCount { get; set; }
		int CurrentPage { get; set; }
		int PageCount { get; set; }

		string Message { get; set; }
		bool Success { get; set; }

		Task<ServiceResponse<AddressDTO>> GetAddress();
        Task<ServiceResponse<bool>> UpdateAddress(AddressDTO addressDTO);

        Task<ServiceResponse<bool>> ChangePassword(ChangePasswordDTO passwordDTO);
        Task<ServiceResponse<bool>> ChangeEmail(ChangeEmailDTO emailDTO);

        Task GetUsers(int page);



		Task<ServiceResponse<EditUserModel>> GetUserForEdit(int Id);
		Task<ServiceResponse<List<DataSelectDTO>>> GetRoles();
		Task<ServiceResponse<bool>> Update(EditUserModel editUserModel);

	}
}
