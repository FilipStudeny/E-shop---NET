using Ecommerce.Shared;
using Ecommerce.Shared.DTOs;
using Ecommerce.Shared.User;

namespace Ecommerce.Server.Services.UserService
{
	public interface IUserService
	{
		Task<ServiceResponse<bool>> RegisterUser(RegisterDTO registerDTO);
		Task<ServiceResponse<string>> Login(LoginDTO loginDTO);
        Task<ServiceResponse<AddressDTO>> GetUser();
		Task<ServiceResponse<List<UserDTO>>> GetUsers(int page);
 
        Task<ServiceResponse<bool>> ChangePassword(ChangePasswordDTO changePasswordDTO);
        Task<ServiceResponse<bool>> ChangeEmail(ChangeEmailDTO changeEmailDTO);
        Task<ServiceResponse<bool>> AddAddress(AddressDTO addressDTO);


        int GetUserId();
		string GetUserEmail();
		Task<bool> UserExists(string Email);



		void CreatePasswordHash(string Password, out byte[] PasswordHash, out byte[] PasswordSalt);
		bool VerifyPasswordHash(string Password, IEnumerable<byte> PasswordHash, byte[] PasswordSalt);
		string CreateToken(User user);

	}
}
