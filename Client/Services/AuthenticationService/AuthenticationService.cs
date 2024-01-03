using System.Net.Http.Json;
using Eshop.Shared.Models;
using Microsoft.AspNetCore.Components.Authorization;

namespace Eshop.Client.Services.AuthenticationService;

public class AuthenticationService : IAuthenticationService
{
    private readonly HttpClient _httpClient;
    private readonly AuthenticationStateProvider _authenticationStateProvider;

    public AuthenticationService(HttpClient httpClient, AuthenticationStateProvider authenticationStateProvider)
    {
        _httpClient = httpClient;
        _authenticationStateProvider = authenticationStateProvider;
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
    
    public async Task<bool> IsAuthenticated()
    {
        return (await _authenticationStateProvider.GetAuthenticationStateAsync()).User.Identity.IsAuthenticated;
    }
}