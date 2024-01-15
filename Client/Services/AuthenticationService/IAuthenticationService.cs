using Ecommerce.Shared;
using Ecommerce.Shared.DTOs;

namespace Ecommerce.Client.Services.AuthenticationService
{
	public interface IAuthenticationService
	{
		Task<ServiceResponse<bool>> Register(RegisterDTO registerDTO);
		Task<ServiceResponse<string>> Login(LoginDTO loginDTO);
		Task<ServiceResponse<bool>> ChangePassword();
		Task<bool> IsAuthenticated();
	}
}
