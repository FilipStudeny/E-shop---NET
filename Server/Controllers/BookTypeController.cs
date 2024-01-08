using Eshop.Server.Services.BookTypeService;
using Eshop.Shared.Models.Books;
using Eshop.Shared.Models.ProductModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eshop.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Admin")]
public class BookTypeController : ControllerBase
{
    private readonly IBookTypeService _bookTypeService;

    public BookTypeController(IBookTypeService bookTypeService)
    {
        _bookTypeService = bookTypeService;
    }
    
    [HttpGet]
    public async Task<ActionResult<ServiceResponse<List<BookType>>>> GetAdminCategories()
    {
        var response = await _bookTypeService.GetBookTypes();
        return Ok(response);
    }
    
    [HttpPut]
    public async Task<ActionResult<ServiceResponse<List<BookType>>>> UpdateBookType(BookType bookType)
    {
        var response = await _bookTypeService.UpdateBookType(bookType);
        return Ok(response);
    }
    
    [HttpPost]
    public async Task<ActionResult<ServiceResponse<List<BookType>>>> AddBookType(BookType bookType)
    {
        var response = await _bookTypeService.AddBookTypes(bookType);
        return Ok(response);
    }
}