using System.Net.Http.Json;
using Eshop.Shared.Models;

namespace Eshop.Client.Services.AuthenticationService;

public class AuthenticationService : IAuthenticationService
{
    private readonly HttpClient _httpClient;

    public AuthenticationService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task<ServiceResponse<int>?> Register(UserRegister userRegister)
    {
        var response = await _httpClient.PostAsJsonAsync("api/authentication/register", userRegister);
        return await response.Content.ReadFromJsonAsync<ServiceResponse<int>>();
    }

    public async Task<ServiceResponse<string>> Login(UserLogin userLogin)
    {
        var response = await _httpClient.PostAsJsonAsync("api/authentication/login", userLogin);
        return await response.Content.ReadFromJsonAsync<ServiceResponse<string>>();
    }

    public async Task<ServiceResponse<bool>> ChangePassword(UserPasswordChange userPasswordChange)
    {
        var response =  await _httpClient.PostAsJsonAsync("api/authentication/passwordchange", userPasswordChange.Password);
        return await response.Content.ReadFromJsonAsync<ServiceResponse<bool>>();

    }
}