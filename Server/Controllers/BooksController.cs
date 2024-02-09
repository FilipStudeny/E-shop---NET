using Ecommerce.Server.Database;
using Ecommerce.Server.Services.BookService;
using Ecommerce.Shared;
using Ecommerce.Shared.Books;
using Ecommerce.Shared.DTOs;
using Ecommerce.Shared.DTOs.Books;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService bookService;
		private readonly DataContext dataContext;

		public BooksController(IBookService bookService, DataContext dataContext)
        {
            this.bookService = bookService;
			this.dataContext = dataContext;
		}

        [HttpGet]
        [Route("{page}")]
        public async Task<ActionResult<ServiceResponse<List<BookDTO>>>> GetBooks([FromQuery(Name = "count")] int count, int page = 1)
        {
            var response = await bookService.GetBooks(page, false ,count);
            return Ok(response);
        }

        [HttpGet]
        [Route("admin/{page}"), Authorize]
        public async Task<ActionResult<ServiceResponse<List<BookDTO>>>> GetAdminBooks([FromQuery(Name = "count")] int count, int page = 1)
        {
            var response = await bookService.GetBooks(page, true, count);
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
        [Route("featured/{page}")]

		public async Task<ActionResult<ServiceResponse<List<BookDTO>>>> GetFeaturedBooks([FromQuery(Name = "count")] int count, int page)
        {
            var response = await bookService.GetFeaturedBooks(page, count);
            return Ok(response);
        }

		[HttpGet]
		[Route("images/{id}")]

		public async Task<ActionResult<ServiceResponse<List<BookDTO>>>> GetFeaturedBooks(int id)
		{
            var response = await dataContext.Images.Where(image => image.BookId == id).ToListAsync();
			return Ok(response);
		}

		[HttpGet]
        [Route("suggestions/{search}")]
        public async Task<ActionResult<ServiceResponse<List<FeaturedBook>>>> GetBookSugestions(string search)
        {
            var response = await bookService.GetBookSuggestions(search);
            return Ok(response);
        }

		[HttpGet]
		[Route("category/{category}/page/{page}")]
		public async Task<ActionResult<ServiceResponse<List<BookDTO>>>> GetBooksByCategory([FromQuery(Name = "count")] int count, int category, int page = 1)
		{
			var response = await bookService.GetBooksByCategory(category, page, count);
			return Ok(response);
		}

		[HttpGet]
		[Route("series/{id}/page/{page}")]
		public async Task<ActionResult<ServiceResponse<List<Book>>>> GetBooksBySeries(int id, int page = 1)
		{
			var response = await bookService.GetBooksBySeries(id, page);
			return Ok(response);
		}


		[HttpPost]
		[Route("admin/book/add")]
		public async Task<ActionResult<ServiceResponse<bool>>> CreateBook(EditBookModel editBookModel)
		{
            var response = await bookService.CreateBook(editBookModel);
			return Ok(response);
		}
	}
}
