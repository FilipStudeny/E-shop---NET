using Eshop.Server.Services.CategoryService;
using Eshop.Shared.Models;
using Eshop.Shared.Models.ProductModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eshop.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<ActionResult<ServiceResponse<List<Category>>>> GetCategories()
    {
        var response = await _categoryService.GetCategories();
        return Ok(response);
    }
    
    [HttpGet]
    [Route("admin"), Authorize(Roles = "Admin")]
    public async Task<ActionResult<ServiceResponse<List<Category>>>> GetAdminCategories()
    {
        var response = await _categoryService.GetAdminCategories();
        return Ok(response);
    }
    
    [HttpDelete]
    [Route("admin/{id}"), Authorize(Roles = "Admin")]
    public async Task<ActionResult<ServiceResponse<List<Category>>>> DeleteCategory(int id)
    {
        var response = await _categoryService.DeleteCategory(id);
        return Ok(response);
    }
    
    [HttpPost]
    [Route("admin"), Authorize(Roles = "Admin")]
    public async Task<ActionResult<ServiceResponse<List<Category>>>> AddCategory(Category category)
    {
        var response = await _categoryService.AddCategory(category);
        return Ok(response);
    }
    
    [HttpPut]
    [Route("admin"), Authorize(Roles = "Admin")]
    public async Task<ActionResult<ServiceResponse<List<Category>>>> UpdateCategory(Category category)
    {
        var response = await _categoryService.UpdateCategory(category);
        return Ok(response);
    }
}