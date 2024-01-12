using Ecommerce.Server.Database;
using Ecommerce.Server.Services.CategoryService;
using Ecommerce.Server.Services.SeriesService;
using Ecommerce.Shared;
using Ecommerce.Shared.Books;
using Ecommerce.Shared.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Server.Services.BookService
{
    public class BookService : IBookService
    {
        private readonly DataContext dataContext;
        private readonly ICategoryService categoryService;
        private readonly ISeriesService seriesService;

        public BookService(DataContext dataContext, ICategoryService categoryService, ISeriesService seriesService)
        {
            this.dataContext = dataContext;
            this.categoryService = categoryService;
            this.seriesService = seriesService;
        }

        public async Task<ServiceResponse<List<Book>>> GetBooks(int page)
        {
            var booksOnPage = 3;
            var bookCount = await dataContext.Books.CountAsync(book => !book.Deleted && book.Visible);
            var pageCount = (int)Math.Ceiling((double)bookCount / booksOnPage);

            var booksFromDb = await dataContext.Books
               .Where(book => !book.Deleted && book.Visible)
               .Include(book => book.Author)
               .Include(book => book.Images)
               .Include(book => book.Variants.Where(variant => variant.Visible && !variant.Deleted))
               .Skip((page - 1) * booksOnPage)
               .Take(booksOnPage)
               .ToListAsync();

            if (booksFromDb == null)
            {
                return new ServiceResponse<List<Book>>
                {
                    Success = false,
                    Message = "No books found."
                };
            }


            return new ServiceResponse<List<Book>>
            {
                Data = booksFromDb,
                NumberOfPages = pageCount,
                CurrentPage = page
            };

        }

        public async Task<ServiceResponse<Book>> GetBook(int id)
        {
            var book = await dataContext.Books
                .Include(book => book.Variants.Where(variant => variant.Visible && !variant.Deleted))
                .ThenInclude(variant => variant.BookType)
                .Include(book => book.Images)
                .Include(book => book.Author)
				.Include(book => book.Series)
				.Include(book => book.Category)
				.FirstOrDefaultAsync(book => book.Id == id && book.Visible && !book.Deleted);

            if (book is null)
            {
                return new ServiceResponse<Book>
                {
                    Success = false,
                    Message = "Book not found."
                };
            }

            return new ServiceResponse<Book> { Data = book };
        }


        public async Task<ServiceResponse<List<FeaturedBook>>> GetFeaturedBooks()
        {
            var booksFromDb = await dataContext.Books
                .Where(book => book.Featured)
                .Include(book => book.Author)
                .Include(book => book.Images) // Ensure Images are included
                .ToListAsync();

            if (booksFromDb == null)
            {
                return new ServiceResponse<List<FeaturedBook>>
                {
                    Success = false,
                    Message = "No books found."
                };
            }

            var featuredBooks = new List<FeaturedBook>();
            foreach (var book in booksFromDb)
            {

                featuredBooks.Add(new FeaturedBook
                {
                    Id = book.Id,
                    Title = book.Title,
                    UrlImage = book.DefaultImageUrl,
                    Image = null,
                    Author = book.Author.Name,
                    AuthorUrl = book.Author.Url
                });
            }

            if (featuredBooks == null)
            {
                return new ServiceResponse<List<FeaturedBook>>
                {
                    Success = false,
                    Message = "No books found."
                };
            }

            return new ServiceResponse<List<FeaturedBook>>
            {
                Data = featuredBooks
            };

        }

        public async Task<ServiceResponse<List<string>>> GetBookSuggestions(string search)
        {
            var books = await GetSuggestedBooks(search);

            if (books == null || books.Count == 0)
            {
                return new ServiceResponse<List<string>>
                {
                    Data = null,
                    Success = false,
                    Message = "No books found."
                };
            }

            List<string> suggestedBooks = new();
            foreach (var book in books)
            {
                var bookTitle = book.Title;
                var bookDescription = book.Description;
                var bookShortDescription = book.ShortDescription;

                if (bookTitle.Contains(search, StringComparison.OrdinalIgnoreCase))
                {
                    suggestedBooks.Add(bookTitle);
                }

                if (bookDescription != null)
                {
                    var suggestions = SearchThroughDescription(bookDescription, search);
                    suggestedBooks.AddRange(suggestions.Except(suggestedBooks, StringComparer.OrdinalIgnoreCase));
                }

                if (bookShortDescription != null)
                {
                    var suggestions = SearchThroughDescription(bookShortDescription, search);
                    suggestedBooks.AddRange(suggestions.Except(suggestedBooks, StringComparer.OrdinalIgnoreCase));
                }
            }

            if (suggestedBooks.Count == 0)
            {
                return new ServiceResponse<List<string>>
                {
                    Data = null,
                    Success = false,
                    Message = "No books found."
                };
            }

            return new ServiceResponse<List<string>> { Data = suggestedBooks };
        }

        private List<string> SearchThroughDescription(string description, string search)
        {
            List<string> suggestions = new();
            if (description != null)
            {
                var punctuation = description.Where(char.IsPunctuation).Distinct().ToArray();
                var words = description.Split().Select(s => s.Trim(punctuation));
                foreach (var word in words)
                {
                    if (word.Contains(search, StringComparison.OrdinalIgnoreCase) && !suggestions.Contains(word))
                    {
                        suggestions.Add(word);
                    }
                }
            }
            return suggestions;
        }

        private async Task<List<Book>> GetSuggestedBooks(string search)
        {
            return await dataContext.Books
                .Where(book =>
                book.Title.ToLower().Contains(search.ToLower())
                || book.Description.ToLower().Contains(search.ToLower())
                || book.ShortDescription.ToLower().Contains(search.ToLower())
                && book.Visible && !book.Deleted)
                .ToListAsync();

        }

        public async Task<ServiceResponse<List<Book>>> GetBooksByCategory(int id, int page)
        {
			
			var booksOnPage = 3;
			var bookCount = await dataContext.Books.CountAsync(book => book.CategoryId == id && !book.Deleted && book.Visible);
			var pageCount = (int)Math.Ceiling((double)bookCount / booksOnPage);

			var booksFromDb = await dataContext.Books
               .Where(book => book.CategoryId == id && !book.Deleted && book.Visible)
               .Include(book => book.Author)
               .Include(book => book.Images)
               .Include(book => book.Variants.Where(variant => variant.Visible && !variant.Deleted))
               .Skip((page - 1) * booksOnPage)
               .Take(booksOnPage)
               .ToListAsync();

            if (booksFromDb == null)
            {
                return new ServiceResponse<List<Book>>
                {
                    Success = false,
                    Message = "No books found."
                };
            }


            return new ServiceResponse<List<Book>>
            {
                Data = booksFromDb,
                NumberOfPages = pageCount,
                CurrentPage = page
            };
        }

        public async Task<ServiceResponse<List<Book>>> GetBooksBySeries(int seriesId, int page)
        {
            {
                var booksOnPage = 3;
                var count = await seriesService.GetSeriesCount();
                var pageCount = (int)Math.Ceiling((double)count / booksOnPage);

                var booksFromDb = await dataContext.Books
                   .Where(book => book.SeriesId == seriesId && !book.Deleted && book.Visible)
                   .Include(book => book.Author)
                   .Include(book => book.Images)
                   .Include(book => book.Variants.Where(variant => variant.Visible && !variant.Deleted))
                   .Skip((page - 1) * booksOnPage)
                   .Take(booksOnPage)
                   .ToListAsync();

                if (booksFromDb == null)
                {
                    return new ServiceResponse<List<Book>>
                    {
                        Success = false,
                        Message = "No books found."
                    };
                }


                return new ServiceResponse<List<Book>>
                {
                    Data = booksFromDb,
                    NumberOfPages = pageCount,
                    CurrentPage = page
                };
            }
        }
    }
}
