using Ecommerce.Client.Services.AuthorsService;
using Ecommerce.Server.Services.CategoryService;
using Ecommerce.Shared;
using Ecommerce.Shared.Books;
using Ecommerce.Shared.DTOs.Authors;
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




		//**EDITS**//
		[HttpGet]
		[Route("admin/{name}")]
		public async Task<ActionResult<ServiceResponse<Category>>> GetAllCategoryNames(string name)
		{
			var response = await categoryService.GetCategoryByUrl(name);
			return Ok(response);
		}

		[HttpGet]
		[Route("admin/all")]
		public async Task<ActionResult<ServiceResponse<List<DataSelectDTO>>>> GetAllCategoryNames()
		{
			var response = await categoryService.GetCategoryNames();
			return Ok(response);
		}

		[HttpPost]
		[Route("admin/add")]
		public async Task<ActionResult<ServiceResponse<bool>>> AddCategory(Category category)
		{
			var response = await categoryService.CreateCategory(category);
			return Ok(response);
		}

		[HttpPut]
		[Route("admin/update")]
		public async Task<ActionResult<ServiceResponse<bool>>> UpdateCategory(Category category)
		{
			var response = await categoryService.UpdateCategory(category);
			return Ok(response);
		}
	}
}
