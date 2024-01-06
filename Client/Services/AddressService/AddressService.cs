using System.Net.Http.Json;
using Eshop.Shared.Models.UserModels;

namespace Eshop.Client.Services.AddressService;

public class AddressService : IAddressService
{
    private readonly HttpClient _httpClient;

    public AddressService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task<Address?> GetAddress()
    {
        ServiceResponse<Address?>? response = await _httpClient.GetFromJsonAsync<ServiceResponse<Address>>("api/user/address");
        return response?.Data;
    }

    public async Task<Address> UpdateAddress(Address address)
    {
        var response = await _httpClient.PostAsJsonAsync("api/User/update/address", address);
        var data = response.Content.ReadFromJsonAsync<ServiceResponse<Address>>().Result.Data;
        return data!;
    }
}