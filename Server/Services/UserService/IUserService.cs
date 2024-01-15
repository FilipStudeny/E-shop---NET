using Ecommerce.Shared;
using Ecommerce.Shared.DTOs;
using Ecommerce.Shared.User;

namespace Ecommerce.Server.Services.UserService
{
	public interface IUserService
	{
		Task<ServiceResponse<bool>> RegisterUser(RegisterDTO registerDTO);
		Task<ServiceResponse<string>> Login(LoginDTO loginDTO);
		Task<ServiceResponse<bool>> ChangePassword(string Email, string newPassword);

		int GetUserId();
		string GetUserEmail();
		Task<User> GetUserByEmail(string Email);
		Task<bool> UserExists(string Email);



		void CreatePasswordHash(string Password, out byte[] PasswordHash, out byte[] PasswordSalt);
		bool VerifyPasswordHash(string Password, IEnumerable<byte> PasswordHash, byte[] PasswordSalt);
		string CreateToken(User user);

	}
}
