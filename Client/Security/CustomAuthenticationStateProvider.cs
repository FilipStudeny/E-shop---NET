using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;

namespace Eshop.Client.Security;

public class CustomAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly ILocalStorageService _localStorageService;
    private readonly HttpClient _httpClient;

    public CustomAuthenticationStateProvider(ILocalStorageService localStorageService, HttpClient httpClient)
    {
        _localStorageService = localStorageService;
        _httpClient = httpClient;
    }
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        string authToken = await _localStorageService.GetItemAsStringAsync("token");

        var identity = new ClaimsIdentity();
        _httpClient.DefaultRequestHeaders.Authorization = null;

        if (!string.IsNullOrEmpty(authToken))
        {
            try
            {
                identity = new ClaimsIdentity(ParseClaimsFromToken(authToken), "jwt");
                if (await NotExpiredToken(identity))
                {
                    _httpClient.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", authToken.Replace("\"", ""));
                }
                else
                {
                    throw new Exception();
                }
                
            }
            catch
            {
                await _localStorageService.RemoveItemAsync("token");
                identity = new ClaimsIdentity();
            }
        }

        var user = new ClaimsPrincipal(identity);
        var state = new AuthenticationState(user);

        NotifyAuthenticationStateChanged(Task.FromResult(state));

        return state;
    }

    // Liberated code
    private byte[] ParseBase64WithoutPadding(string base64)
    {
        switch (base64.Length % 4)
        {
            case 2: base64 += "=="; break;
            case 3: base64 += "="; break;
        }
        return Convert.FromBase64String(base64);
    }

    private IEnumerable<Claim> ParseClaimsFromToken(string token)
    {
        var payload = token.Split('.')[1];
        var jsonBytes = ParseBase64WithoutPadding(payload);
        var keyPair = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

        var claims = keyPair.Select(kv => new Claim(kv.Key, kv.Value.ToString()!));
        return claims;
    }
    
    public async Task<bool> NotExpiredToken(ClaimsIdentity? identity)
    {
        if (identity == null) return false;
        
        var user = new ClaimsPrincipal(identity);
        var exp = user.FindFirst("exp");
        
        if (exp == null) return false;
        var expDateTime = DateTimeOffset.FromUnixTimeSeconds(long.Parse(exp.Value));
        return DateTime.Now <= expDateTime;
    }
}