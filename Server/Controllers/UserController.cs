using Ecommerce.Server.Services.UserService;
using Ecommerce.Shared;
using Ecommerce.Shared.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<AddressDTO>>> GetUser()
        {
            var response = await userService.GetUser();
            return Ok(response);
        }

        [HttpPut]
		[Route("change/address")]

		public async Task<ActionResult<ServiceResponse<bool>>> AddShippingAddress(AddressDTO addressDTO)
        {
            var response = await userService.AddAddress(addressDTO);
            return Ok(response);
        }

        [HttpPut]
        [Route("change/password")]
        public async Task<ActionResult<ServiceResponse<bool>>> ChangeUserPassword(ChangePasswordDTO changePasswordDTO)
        {
            var response = await userService.ChangePassword(changePasswordDTO);
            return Ok(response);
        }

        [HttpPut]
        [Route("change/email")]

        public async Task<ActionResult<ServiceResponse<bool>>> ChangeUserEmail(ChangeEmailDTO changeEmailDTO)
        {
            var response = await userService.ChangeEmail(changeEmailDTO);
            return Ok(response);
        }

    }
}
