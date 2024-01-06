using Eshop.Server.Services.UserService;
using Eshop.Shared.Models.UserModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eshop.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    [Route("address")]
    public async Task<ActionResult<ServiceResponse<Address>>> GetAddress()
    {
        return await _userService.GetAddress();
    }

    [HttpPost]
    [Route("update/address")]
    public async Task<ActionResult<ServiceResponse<Address>>> UpdateAddress(Address address)
    {
        return await _userService.UpdateAddress(address);
    }
}