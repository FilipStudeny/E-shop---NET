using Ecommerce.Shared;
using Ecommerce.Shared.DTOs;
using System.Net.Http.Json;

namespace Ecommerce.Client.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly HttpClient httpClient;

        public UserService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
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
