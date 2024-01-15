using Ecommerce.Shared;
using Ecommerce.Shared.DTOs;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;

namespace Ecommerce.Client.Services.AuthenticationService
{
	public class AuthenticationService : IAuthenticationService
	{
		private readonly HttpClient httpClient;
		private readonly AuthenticationStateProvider authenticationStateProvider;

		public AuthenticationService(HttpClient httpClient, AuthenticationStateProvider authenticationStateProvider)
        {
			this.httpClient = httpClient;
			this.authenticationStateProvider = authenticationStateProvider;
		}

		public async Task<ServiceResponse<string>> Login(LoginDTO loginDTO)
		{
			var response = await httpClient.PostAsJsonAsync("api/auth/login", loginDTO);
			var responseContent = await response.Content.ReadFromJsonAsync<ServiceResponse<string>>();
			if (responseContent == null)
			{
				return new ServiceResponse<string> { Success = false, Message = "Error login in, try again later" };
			}
			return responseContent;

		}

		public async Task<ServiceResponse<bool>> Register(RegisterDTO registerDTO)
		{
			var response = await httpClient.PostAsJsonAsync("api/auth/register", registerDTO);
			var responseContent = await response.Content.ReadFromJsonAsync<ServiceResponse<bool>>();
			if(responseContent == null)
			{
				return new ServiceResponse<bool> { Success = false, Message = "Error while creating new account, try again later" };
			}
			return responseContent;

		}

		public Task<ServiceResponse<bool>> ChangePassword()
		{
			throw new NotImplementedException();
		}

		public async Task<bool> IsAuthenticated()
		{
			var authenticationState = await authenticationStateProvider.GetAuthenticationStateAsync();

			if (authenticationState != null && authenticationState.User != null && authenticationState.User.Identity != null)
			{
				var isAuthenticated = authenticationState.User.Identity.IsAuthenticated;
				return isAuthenticated;
			}

			return false;
		}


	}
}
