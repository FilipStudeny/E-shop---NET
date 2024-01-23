using Ecommerce.Server.Database;
using Ecommerce.Server.Services.CategoryService;
using Ecommerce.Server.Services.SeriesService;
using Ecommerce.Shared;
using Ecommerce.Shared.Books;
using Ecommerce.Shared.DTOs;
using Ecommerce.Shared.DTOs.Books;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Server.Services.BookService
{
    public class BookService : IBookService
    {
        private readonly DataContext dataContext;
        private readonly ISeriesService seriesService;

        public BookService(DataContext dataContext, ISeriesService seriesService)
        {
            this.dataContext = dataContext;
            this.seriesService = seriesService;
        }

        public async Task<ServiceResponse<List<BookDTO>>> GetBooks(int page, bool getAll = false,int numberOfItems = 5)
        {
            var booksOnPage = numberOfItems;
            var bookCount = getAll == false ?
                await dataContext.Books.CountAsync(book => !book.Deleted && book.Visible) :
                await dataContext.Books.CountAsync();

            var pageCount = (int)Math.Ceiling((double)bookCount / booksOnPage);
            var booksFromDb = getAll == false ?
                await dataContext.Books
                   .Where(book => !book.Deleted && book.Visible)
                   .Include(book => book.Author)
                   .Include(book => book.Images)
                   .Include(book => book.Variants.Where(variant => variant.Visible && variant.Deleted == getAll))
                   .Skip((page - 1) * booksOnPage)
                   .Take(booksOnPage)
                   .ToListAsync():
				await dataContext.Books
				   .Include(book => book.Author)
				   .Include(book => book.Images)
				   .Include(book => book.Variants.Where(variant => variant.Visible && variant.Deleted == getAll))
				   .Skip((page - 1) * booksOnPage)
				   .Take(booksOnPage)
				   .ToListAsync();

			if (booksFromDb == null)
            {
                return new ServiceResponse<List<BookDTO>>
                {
                    Success = false,
                    Message = "No books found."
                };
            }

            var books = booksFromDb.Select(book => new BookDTO
            {
                Id = book.Id,
                Title = book.Title,
                Image = book.DefaultImageUrl,
                AuthorName = book?.Author?.Name!,
                AuthorUrl = book?.Author?.Url!,
                Price = book?.Variants?.FirstOrDefault()?.Price ?? 0.0m
            }).ToList();



			return new ServiceResponse<List<BookDTO>>
            {
                Data = books,
                NumberOfPages = pageCount,
                CurrentPage = page,
                ItemCount = bookCount
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


        public async Task<ServiceResponse<List<BookDTO>>> GetFeaturedBooks(int page, int numberOfItems = 5)
        {

			var booksOnPage = numberOfItems;
            var bookCount = await dataContext.Books.CountAsync(book => !book.Deleted && book.Visible && book.Featured); 
			var pageCount = (int)Math.Ceiling((double)bookCount / booksOnPage);

			var booksFromDb = await dataContext.Books
                .Where(book => book.Featured && book.Visible && !book.Deleted)
                .Include(book => book.Author)
                .Include(book => book.Images) // Ensure Images are included
				.Include(book => book.Variants.Where(variant => variant.Visible && !variant.Deleted))
				.Skip((page - 1) * booksOnPage)
				.Take(booksOnPage)
                .ToListAsync();

			if (booksFromDb == null)
			{
				return new ServiceResponse<List<BookDTO>>
				{
					Success = false,
					Message = "No books found."
				};
			}

			var books = booksFromDb.Select(book => new BookDTO
			{
				Id = book.Id,
				Title = book.Title,
				Image = book.DefaultImageUrl,
				AuthorName = book?.Author?.Name!,
				AuthorUrl = book?.Author?.Url!,
				Price = book?.Variants?.FirstOrDefault()?.Price ?? 0.0m
			}).ToList();

            if(books == null)
            {
				return new ServiceResponse<List<BookDTO>>
				{
					Success = false,
					Message = "No books found."
				};
			}

            return new ServiceResponse<List<BookDTO>>
            {
                Data = books,
				NumberOfPages = pageCount,
				CurrentPage = page,
				ItemCount = bookCount
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

        public async Task<List<Book>> GetBooksByAuthor(int authorId)
        {
            var books = await dataContext.Books.Where(book => book.AuthorId == authorId && book.Visible && !book.Deleted).ToListAsync();
            if(books == null)
            {
                return new();
            }

            return books;
        }

		public async Task<List<Book>> GetBooksInSeries(int seriesId)
		{
            var books = await dataContext.Books.Where(book => book.SeriesId == seriesId && book.Visible && !book.Deleted).ToListAsync();
            return books;
		}


	}
}
