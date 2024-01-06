using Eshop.Server.Database;
using Eshop.Server.Services.Authentication;
using Eshop.Shared.Models.UserModels;
using Microsoft.EntityFrameworkCore;

namespace Eshop.Server.Services.UserService;

public class UserService : IUserService
{
    private readonly DataContext _dataContext;
    private readonly IAuthenticationService _authenticationService;

    public UserService(DataContext dataContext,IAuthenticationService authenticationService)
    {
        _dataContext = dataContext;
        _authenticationService = authenticationService;
    }
    public async Task<ServiceResponse<Address>> GetAddress()
    {
        var userId =  _authenticationService.GetUserId();
        var address = await _dataContext.Addresses.FirstOrDefaultAsync(address => address.UserId == userId);
        return new ServiceResponse<Address> { Data = address };
    }

    public async Task<ServiceResponse<Address>> UpdateAddress(Address address)
    {
        var response = new ServiceResponse<Address>();
        var userAddress = (await GetAddress()).Data;

        if (userAddress == null)
        {
            address.UserId = _authenticationService.GetUserId();
            _dataContext.Addresses.Add(address);
            response.Data = address;
        }
        else
        {
            userAddress.FirstName = address.FirstName;
            userAddress.LastName = address.LastName;
            userAddress.City = address.City;
            userAddress.Street = address.Street;
            userAddress.Country = address.Country;
            userAddress.Zip = address.Zip;
            response.Data = userAddress;
        }

        await _dataContext.SaveChangesAsync();
        return response;
    }
}