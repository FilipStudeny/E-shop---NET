using Ecommerce.Server.Services.BookTypeService;
using Ecommerce.Shared.DTOs.Books;
using Ecommerce.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Server.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TypeController : ControllerBase
	{
		private readonly IBookTypeService bookTypeService;

		public TypeController(IBookTypeService bookTypeService)
        {
			this.bookTypeService = bookTypeService;
		}

		[HttpGet]
		public async Task<ActionResult<ServiceResponse<List<BookDTO>>>> GetTypes()
		{
			var response = await bookTypeService.GetBookTypes();
			return Ok(response);
		}
	}
}
