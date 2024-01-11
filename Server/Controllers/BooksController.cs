using Ecommerce.Server.Services.BookService;
using Ecommerce.Shared;
using Ecommerce.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService bookService;

        public BooksController(IBookService bookService)
        {
            this.bookService = bookService;
        }

        [HttpGet]
        [Route("{page}")]
        public async Task<ActionResult<ServiceResponse<List<FeaturedBook>>>> GetBooks(int page = 1)
        {
            var response = await bookService.GetBooks(page);
            return Ok(response);
        }

        [HttpGet]
        [Route("book/{id}")]
        public async Task<ActionResult<ServiceResponse<List<FeaturedBook>>>> GetBook(int id)
        {
            var response = await bookService.GetBook(id);
            return Ok(response);
        }


        [HttpGet]
        [Route("featured")]
        public async Task<ActionResult<ServiceResponse<List<FeaturedBook>>>> GetFeaturedBooks()
        {
            var response = await bookService.GetFeaturedBooks();
            return Ok(response);
        }

        [HttpGet]
        [Route("suggestions/{search}")]
        public async Task<ActionResult<ServiceResponse<List<FeaturedBook>>>> GetBookSugestions(string search)
        {
            var response = await bookService.GetBookSuggestions(search);
            return Ok(response);
        }
    }
}
