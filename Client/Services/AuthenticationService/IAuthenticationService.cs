using Eshop.Shared.Models;

namespace Eshop.Client.Services.AuthenticationService;

public interface IAuthenticationService
{
    Task<ServiceResponse<int>?> Register(UserRegister userRegister);
    Task<ServiceResponse<string>> Login(UserLogin userLogin);
    Task<ServiceResponse<bool>> ChangePassword(UserPasswordChange userPasswordChange);
}