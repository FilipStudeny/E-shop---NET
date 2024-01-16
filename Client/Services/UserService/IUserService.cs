using Ecommerce.Shared;
using Ecommerce.Shared.DTOs;

namespace Ecommerce.Client.Services.UserService
{
    public interface IUserService
    {
        Task<ServiceResponse<AddressDTO>> GetAddress();
        Task<ServiceResponse<bool>> UpdateAddress();

        Task<ServiceResponse<bool>> ChangePassword(ChangePasswordDTO passwordDTO);
        Task<ServiceResponse<bool>> ChangeEmail(ChangeEmailDTO emailDTO);

    }
}
