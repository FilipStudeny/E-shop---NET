using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;

namespace Ecommerce.Client.Security
{
	public class CustomAuthenticationStateProvider : AuthenticationStateProvider
	{
		private readonly ILocalStorageService localStorageService;
		private readonly HttpClient httpClient;

		public CustomAuthenticationStateProvider(ILocalStorageService localStorageService, HttpClient httpClient)
        {
			this.localStorageService = localStorageService;
			this.httpClient = httpClient;
		}



		public override async Task<AuthenticationState> GetAuthenticationStateAsync()
		{
			string token = await localStorageService.GetItemAsStringAsync("token");
			var identity = new ClaimsIdentity();
			httpClient.DefaultRequestHeaders.Authorization = null;

			if (!string.IsNullOrEmpty(token))
			{
				try
				{
					identity = new ClaimsIdentity(ParseClaimsFromToken(token), "jwt");

					if(NotExpiredToken(identity))
					{
						var authenticationValue = new AuthenticationHeaderValue("Bearer", token.Replace("\"", ""));
						httpClient.DefaultRequestHeaders.Authorization = authenticationValue;
					}
					else
					{
						throw new Exception();
					}

				}catch
				{
					await localStorageService.RemoveItemAsync("token");
					identity = new ClaimsIdentity();
				}
			}

			var user = new ClaimsPrincipal(identity);
			var state = new AuthenticationState(user);
			NotifyAuthenticationStateChanged(Task.FromResult(state));
			return state;
		}

		public async Task Loggout()
		{
			await localStorageService.RemoveItemAsync("token");

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(new ClaimsPrincipal())));
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

			var claims = keyPair!.Select(kv => new Claim(kv.Key, kv.Value.ToString()!));
			return claims;
		}

		public bool NotExpiredToken(ClaimsIdentity? identity)
		{
			if (identity == null) return false;

			var user = new ClaimsPrincipal(identity);
			var exp = user.FindFirst("exp");

			if (exp == null) return false;
			var expDateTime = DateTimeOffset.FromUnixTimeSeconds(long.Parse(exp.Value));
			return DateTime.Now <= expDateTime;
		}
	}
}
