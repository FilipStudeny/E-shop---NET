using System.Security.Claims;
using Eshop.Server.Services.Authentication;
using Eshop.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eshop.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost]
    [Route("register")]
    public async Task<ActionResult<ServiceResponse<int>>> RegisterUser(UserRegister userRegister)
    {
        var response = await _authenticationService.Register(new User {Email = userRegister.Email }, userRegister.Password);
        if (!response.Success)
        {
            return BadRequest(response);
        }
        
        return Ok(response);
    }
    
    [HttpPost]
    [Route("login")]
    public async Task<ActionResult<ServiceResponse<int>>> LoginUser(UserLogin userLogin)
    {
        var response = await _authenticationService.Login(userLogin.Email, userLogin.Password);
        if (!response.Success)
        {
            return BadRequest(response);
        }
        
        return Ok(response);
    }

    [HttpPost]
    [Route("passwordchange")]
    [Authorize]
    public async Task<ActionResult<ServiceResponse<bool>>> ChangePassword([FromBody] string newPassword)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var response = await _authenticationService.ChangePassword(int.Parse(userId!), newPassword);
        if (!response.Success)
        {
            return BadRequest(response);
        }

        return Ok(response);
    }
}