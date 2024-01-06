using Eshop.Shared.Models.UserModels;

namespace Eshop.Client.Services.AddressService;

public interface IAddressService
{
    Task<Address?> GetAddress();
    Task<Address> UpdateAddress(Address address);
}