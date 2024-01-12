using Ecommerce.Server.Services.AuthorsService;
using Ecommerce.Shared;
using Ecommerce.Shared.Books;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Server.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthorsController : ControllerBase
	{
		private readonly IAuthorsService authorsService;

		public AuthorsController(IAuthorsService authorsService)
        {
			this.authorsService = authorsService;
		}

		[HttpGet]
		[Route("{page}")]
		public async Task<ActionResult<ServiceResponse<List<Author>>>> GetAuthors(int page)
		{
			var response = await authorsService.GetAuthors(page);
			return Ok(response);
		}


		[HttpGet]
		[Route("author/{name}")]
		public async Task<ActionResult<ServiceResponse<Author>>> GetAuthor(string name)
		{
			var response = await authorsService.GetAuthor(name);
			return Ok(response);
		}
	}
}
