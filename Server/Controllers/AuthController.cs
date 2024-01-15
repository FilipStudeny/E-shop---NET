using Ecommerce.Server.Services.UserService;
using Ecommerce.Shared.User;
using Ecommerce.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ecommerce.Shared.DTOs;

namespace Ecommerce.Server.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly IUserService userService;

		public AuthController(IUserService userService)
        {
			this.userService = userService;
		}

		[HttpPost]
		[Route("register")]
		public async Task<ActionResult<ServiceResponse<bool>>> RegisterUser(RegisterDTO registerDTO)
		{
			var response = await userService.RegisterUser(registerDTO);
			return Ok(response);
		}

		[HttpPost]
		[Route("login")]
		public async Task<ActionResult<ServiceResponse<string>>> LoginUser(LoginDTO loginDTO)
		{
			var response = await userService.Login(loginDTO);
			return Ok(response);
		}
	}
}
