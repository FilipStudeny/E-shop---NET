using Ecommerce.Shared;
using Ecommerce.Shared.Books;
using Ecommerce.Shared.DTOs;
using Ecommerce.Shared.DTOs.Books;

namespace Ecommerce.Client.Services.BookService
{
    public interface IBookService
    {
        event Action? OnChange;
        List<BookDTO> Books { get; set; }

        int BookCount { get; set; }
        int CurrentPage { get; set; }
        int PageCount { get; set; } 

        string Message { get; set; }
        bool Success { get; set; }

        Task GetBooks(int page, bool evenDeleted = false);
		Task GetBooksByCategory(int page, int category);
		Task GetFeaturedBooks(int page);


		Task<ServiceResponse<Book>> GetBook(int id);
        Task<List<string>> GetSuggestedBooks(string search);

        Task<ServiceResponse<bool>> EditBook(EditBookModel editBookModel);
    }
}
