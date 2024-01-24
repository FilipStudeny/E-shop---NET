using Ecommerce.Server.Services.CategoryService;
using Ecommerce.Shared;
using Ecommerce.Shared.Books;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Server.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CategoryController : ControllerBase
	{
		private readonly ICategoryService categoryService;

		public CategoryController(ICategoryService categoryService)
        {
			this.categoryService = categoryService;
		}


		[HttpGet]
		public async Task<ActionResult<ServiceResponse<List<Category>>>> GetCategories()
		{
			var response = await categoryService.GetCategories();
			return Ok(response);
		}

		[HttpGet]
		[Route("admin"), Authorize]
		public async Task<ActionResult<ServiceResponse<List<Category>>>> GetAllCategories()
		{
			var response = await categoryService.GetCategories(true);
			return Ok(response);
		}
	}
}
