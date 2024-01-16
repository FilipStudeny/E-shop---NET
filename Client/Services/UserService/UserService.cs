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

        public Task<ServiceResponse<bool>> UpdateAddress()
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<bool>> ChangeEmail(ChangeEmailDTO emailDTO)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<bool>> ChangePassword(ChangePasswordDTO passwordDTO)
        {
            throw new NotImplementedException();
        }

        
    }
}
