using Eshop.Server.Services.BookService;
using Eshop.Shared.DTOs;
using Eshop.Shared.Models.Books;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eshop.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BookController : ControllerBase
{
    private readonly IBookService _bookService;

    public BookController(IBookService bookService)
    {
        _bookService = bookService;
    }
    
    
    [HttpGet]
    public async Task<ActionResult<ServiceResponse<List<Book>>>> GetBooks()
    {
        var response = await _bookService.GetBooks();
        return Ok(response);
    }
    
    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult<ServiceResponse<Book>>> GetBook(int id)
    {
        var response = await _bookService.GetBook(id);
        return Ok(response);
    }
    
    [HttpGet]
    [Route("category/{category}")]
    public async Task<ActionResult<ServiceResponse<Book>>> GetBooksByCategory(string category)
    {
        var response = await _bookService.GetBooksByCategory(category);
        return Ok(response);
    }
    
    [HttpGet]
    [Route("search/{search}/{page}")]
    public async Task<ActionResult<ServiceResponse<BookSearchDto>>> SearchForBook(string search, int page = 1)
    {
        var response = await _bookService.SearchForBooks(search, page);
        return Ok(response);
    }
    
    [HttpGet]
    [Route("suggestions/{search}")]
    public async Task<ActionResult<ServiceResponse<Book>>> SearchBookSuggestions(string search)
    {
        var response = await _bookService.GetBooksSearchSuggestions(search);
        return Ok(response);
    }
    
    [HttpGet]
    [Route("featured")]
    public async Task<ActionResult<ServiceResponse<Book>>> GetFeaturedBooks()
    {
        var response = await _bookService.GetFeaturedBooks();
        return Ok(response);
    }
    
    [HttpGet]
    [Route("admin"), Authorize(Roles = "Admin")]
    public async Task<ActionResult<ServiceResponse<List<Book>>>> GetAdminBooks()
    {
        var response = await _bookService.GetAdminBooks();
        return Ok(response);
    }
    
    [HttpPost]
    [Route("admin"), Authorize(Roles = "Admin")]
    public async Task<ActionResult<ServiceResponse<Book>>> CreateBook(Book book)
    {
        var response = await _bookService.CreateBook(book);
        return Ok(response);
    }
    
    [HttpPut]
    [Route("admin"), Authorize(Roles = "Admin")]
    public async Task<ActionResult<ServiceResponse<Book>>> UpdateBook(Book book)
    {
        var response = await _bookService.UpdateBook(book);
        return Ok(response);
    }
    
    [HttpDelete]
    [Route("admin/{id}"), Authorize(Roles = "Admin")]
    public async Task<ActionResult<ServiceResponse<bool>>> DeleteBook(int id)
    {
        var response = await _bookService.DeleteBook(id);
        return Ok(response);
    }
}