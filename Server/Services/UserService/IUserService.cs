using Eshop.Shared.Models.UserModels;

namespace Eshop.Server.Services.UserService;

public interface IUserService
{
    Task<ServiceResponse<Address>> GetAddress();
    Task<ServiceResponse<Address>> UpdateAddress(Address address);
}