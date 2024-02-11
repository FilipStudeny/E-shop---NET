using Ecommerce.Server.Services.UserService;
using Ecommerce.Shared;
using Ecommerce.Shared.DTOs;
using Ecommerce.Shared.DTOs.Authors;
using Ecommerce.Shared.DTOs.Users;
using Ecommerce.Shared.User;
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


		[HttpGet]
        [Route("admin/{page}")]
		public async Task<ActionResult<ServiceResponse<AddressDTO>>> GetUsers(int page)
		{
			var response = await userService.GetUsers(page);
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


		[HttpGet]
		[Route("roles")]
		public async Task<ActionResult<ServiceResponse<List<DataSelectDTO>>>> GetRoles()
		{
            var response = await userService.GetRoles();
			return Ok(response);
		}

		[HttpGet]
		[Route("admin/user/{id}")]

		public async Task<ActionResult<ServiceResponse<EditUserModel>>> GetUserForEdit(int Id)
		{
            var response = await userService.GetUserForEdit(Id);
			return Ok(response);
		}

		[HttpPut]
		[Route("admin/update")]

		public async Task<ActionResult<ServiceResponse<bool>>> UpdateUser(EditUserModel editUserModel)
		{
            var response = await userService.UpdateUser(editUserModel);
			return Ok(response);
		}

	}
}
