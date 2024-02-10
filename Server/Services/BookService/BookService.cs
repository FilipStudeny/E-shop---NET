using Ecommerce.Client.Pages;
using Ecommerce.Server.Database;
using Ecommerce.Server.Services.CategoryService;
using Ecommerce.Server.Services.SeriesService;
using Ecommerce.Shared;
using Ecommerce.Shared.Books;
using Ecommerce.Shared.DTOs;
using Ecommerce.Shared.DTOs.Books;
using Microsoft.EntityFrameworkCore;
using MudBlazor;

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

			foreach (var book in booksFromDb)
			{
				if (book.Images != null && book.Images.Count > 0)
				{
					book.DefaultImageUrl = book.Images[0].Data;
				}
			}

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
                .Include(book => book.Author)
                .Include(book => book.Images)
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

            if(book.Images != null && book.Images.Count > 0)
            {
				book.DefaultImageUrl = book.Images[0].Data;

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


			foreach (var book in booksFromDb)
			{
				if (book.Images != null && book.Images.Count > 0)
				{
					book.DefaultImageUrl = book.Images[0].Data;
				}
			}

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

        public async Task<ServiceResponse<List<BookDTO>>> GetBooksByCategory(int id, int page, int numberOfItems = 5)
		{

			var booksOnPage = numberOfItems;
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


			foreach (var book in booksFromDb)
			{
				if (book.Images != null && book.Images.Count > 0)
				{
					book.DefaultImageUrl = book.Images[0].Data;
				}
			}

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

				foreach (var book in booksFromDb)
				{
					if (book.Images != null && book.Images.Count > 0)
					{
						book.DefaultImageUrl = book.Images[0].Data;
					}
				}

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
            var books = await dataContext.Books.Where(book => book.SeriesId == seriesId && book.Visible && !book.Deleted)
				.ToListAsync();

			return books;
		}

		public async Task<ServiceResponse<bool>> CreateBook(EditBookModel editBookModel)
		{
            try
            {
				var book = new Book
				{
					Title = editBookModel.Title,
					ShortDescription = editBookModel.ShortDescription,
					Description = editBookModel.Description,
					ReleaseDate = editBookModel.ReleaseDate,
					DateAdded = DateTime.Now,
					PageCount = editBookModel.PageCount,
					CopiesInStore = editBookModel.CopiesInStore,
					AuthorId = editBookModel.Author.Id,
					SeriesId = editBookModel.Series.Id,
                    SeriesOrder = editBookModel.SeriesOrder,
					CategoryId = editBookModel.Category.Id,
					Isbn = editBookModel.Isbn,
                    Visible = editBookModel.Visible,
                    Featured = editBookModel.Featured
				};

				dataContext.Books.Add(book);
				await dataContext.SaveChangesAsync();

				var imageList = new List<Image>();
				foreach (var image in editBookModel.Images)
				{
                    var newImage = new Image{ 
                        Data = image.Data, 
                        BookId = book.Id 
                    };

                    dataContext.Images.Add(newImage);
				}

				foreach (var variant in editBookModel?.Variants!)
				{
					var newVariant = new BookVariant
					{
						BookId = book.Id,
						BookTypeId = variant.BookTypeId,
						OriginalPrice = variant.OriginalPrice,
						Price = variant.Price
					};

                    dataContext.BookVariants.Add(newVariant);
				}

				await dataContext.SaveChangesAsync();
				return new ServiceResponse<bool> { Data = true };
			}
            catch
            {
				return new ServiceResponse<bool> { Data = false, Success = false, Message = "Error, couldn't create new book" };
			}
		}


		public async Task<ServiceResponse<bool>> UpdateBook(EditBookModel editBookModel)
		{
            int bookId = editBookModel.Id;
            var book = await dataContext.Books.FindAsync(bookId);
            if(book == null)
            {
				return new ServiceResponse<bool> { Data = false, Success = false, Message = "Couldn't update book, book with an Id: " + bookId + " not found" };
			}

			// GET IMAGES FOR BOOK, CREATE LIST OF IMAGES TO REMOVE, ADD NEW IMAGES
			var bookImages = await dataContext.Images.Where(image => image.BookId == bookId).ToListAsync();
            var editedImages = editBookModel.Images;
            var imagesToRemove = bookImages.Where(existingImage => !editedImages.Any(editedImage => editedImage.Id == existingImage.Id && !editedImage.IsNew)).ToList();
            dataContext.Images.RemoveRange(imagesToRemove);

			var newImages = editedImages
                .Where(image => image.IsNew)
				.Select(image => new Image
				{
					Data = image.Data,
					BookId = bookId
				})
				.ToList();
			dataContext.Images.AddRange(newImages);

			// GET BOOK OLD BOOK VARIANTS, REMOVE THEM AND ADD NEW ONES
			var bookVariants = await dataContext.BookVariants.Where(variant => variant.BookId == bookId).ToListAsync();
            var editedVariants = editBookModel.Variants;
            var variantsToRemove = bookVariants.Where(existingVariant => !editedVariants!.Any(editedVariant => editedVariant.Id == existingVariant.Id && !editedVariant.IsNew)).ToList();
			dataContext.BookVariants.RemoveRange(variantsToRemove);
			var newVariants = editedVariants!
				.Where(v => v.IsNew)
	            .Select(v => new BookVariant
	            {
					BookId = bookId,
					BookTypeId = v.BookTypeId,
					OriginalPrice = v.OriginalPrice,
					Price = v.Price
				})
	            .ToList();
			dataContext.BookVariants.AddRange(newVariants);

			//UPDATE BOOK DATA
			book.Title = editBookModel.Title;
			book.ShortDescription = editBookModel.ShortDescription;
			book.Description = editBookModel.Description;
			book.DefaultImageUrl = editBookModel.Images[0].Data;
			book.DateAdded = DateTime.Now;
			book.ReleaseDate = editBookModel.ReleaseDate;
			book.PageCount = editBookModel.PageCount;
			book.CopiesInStore = editBookModel.CopiesInStore;
			book.SeriesOrder = editBookModel.SeriesOrder;
			book.AuthorId = editBookModel.Author.Id;
			book.SeriesId = editBookModel.Series.Id;
			book.CategoryId = editBookModel.Category.Id;
			book.Isbn = editBookModel.Isbn;
			book.Featured = editBookModel.Featured;
			book.Visible = editBookModel.Visible;

            dataContext.Books.Update(book);
			await dataContext.SaveChangesAsync();

			return new ServiceResponse<bool> { Data = true, Message = "Book updated" };	
        }
	}
}
