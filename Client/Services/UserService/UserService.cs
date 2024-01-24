using Ecommerce.Client.Pages;
using Ecommerce.Shared;
using Ecommerce.Shared.DTOs;
using Ecommerce.Shared.DTOs.Books;
using System.Net.Http.Json;

namespace Ecommerce.Client.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly HttpClient httpClient;

		public event Action? OnChange;

		public List<UserDTO> Users { get ; set; }
		public int UsersCount { get; set; }
		public int CurrentPage { get; set; }
		public int PageCount { get; set; }
		public string Message { get; set; } = "Loading ...";
		public bool Success { get; set; }

		public UserService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task GetUsers(int page)
        {
			var response = await httpClient.GetFromJsonAsync<ServiceResponse<List<UserDTO>>>($"api/user/admin/{page}");
			if (response is { Data: not null })
			{
				Users = response.Data;
				CurrentPage = response.CurrentPage;
				PageCount = response.NumberOfPages;
				UsersCount = response.ItemCount;
			}

			if (Users.Count == 0)
			{
				Message = "No users found.";
			}

			OnChange?.Invoke();
		}

        public async Task<ServiceResponse<AddressDTO>> GetAddress()
        {
            var response = await httpClient.GetFromJsonAsync<ServiceResponse<AddressDTO>>("api/user");
            if (response == null)
            {
                return new ServiceResponse<AddressDTO> { Success = false, Message = "Error retrieving address, try again later" };
            }
            return response;
        }

        public async Task<ServiceResponse<bool>> UpdateAddress(AddressDTO addressDTO)
        {
			var response = await httpClient.PutAsJsonAsync("api/user/change/address", addressDTO);
			var responseData = (await response.Content.ReadFromJsonAsync<ServiceResponse<bool>>());
			if (responseData == null)
			{
				return new ServiceResponse<bool> { Success = false, Message = "Failed to change shipping address, try again later" };
			}
			return responseData;
		}

        public async Task<ServiceResponse<bool>> ChangeEmail(ChangeEmailDTO emailDTO)
        {
			var response = await httpClient.PutAsJsonAsync("api/user/change/email", emailDTO);
            var responseData = (await response.Content.ReadFromJsonAsync<ServiceResponse<bool>>());
			if (responseData == null)
			{
				return new ServiceResponse<bool> { Success = false, Message = "Failed to change email address, try again later" };
			}
			return responseData;
		}

        public async Task<ServiceResponse<bool>> ChangePassword(ChangePasswordDTO passwordDTO)
        {
			var response = await httpClient.PutAsJsonAsync("api/user/change/password", passwordDTO);
			var responseData = (await response.Content.ReadFromJsonAsync<ServiceResponse<bool>>());
			if (responseData == null)
			{
				return new ServiceResponse<bool> { Success = false, Message = "Error changing password, try again later" };
			}
			return responseData;
		}

        
    }
}
