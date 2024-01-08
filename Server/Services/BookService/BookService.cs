using Eshop.Server.Database;
using Eshop.Server.Services.Authentication;
using Eshop.Shared.DTOs;
using Eshop.Shared.DTOs.Books;
using Eshop.Shared.Models.Books;
using Microsoft.EntityFrameworkCore;

namespace Eshop.Server.Services.BookService;

public class BookService : IBookService
{
    private readonly DataContext _dataContext;
    private readonly IAuthenticationService _authenticationService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public BookService(DataContext dataContext, IAuthenticationService authenticationService, IHttpContextAccessor httpContextAccessor)
    {
        _dataContext = dataContext;
        _authenticationService = authenticationService;
        _httpContextAccessor = httpContextAccessor;
    }
    
    public async Task<ServiceResponse<List<Book>>> GetBooks()
    {

        var response = new ServiceResponse<List<Book>>
        {
            Data = await _dataContext.Books
                .Where(p => p.Visible && !p.Deleted)
                .Include(p => p.Variants.Where(v => v.Visible && !v.Deleted))
                .Include(p => p.Images)
                .ToListAsync()
        };

        return response;
        
    }

    public async Task<ServiceResponse<Book>> GetBook(int id)
    {
        var response = new ServiceResponse<Book>();
        Book? product = null;

        if (_httpContextAccessor.HttpContext != null && _httpContextAccessor.HttpContext.User.IsInRole("Admin"))
        {
            product  = await _dataContext.Books
                .Include(p => p.Variants.Where(v => !v.Deleted))
                .ThenInclude(variant => variant.BookType)
                .Include(p => p.Images)
                .FirstOrDefaultAsync(p => p.Id == id && !p.Deleted);
        }
        else
        {
            product  = await _dataContext.Books
                .Include(p => p.Variants.Where(v => v.Visible && !v.Deleted))
                .ThenInclude(variant => variant.BookType)
                .Include(p => p.Images)
                .FirstOrDefaultAsync(p => p.Id == id && p.Visible && !p.Deleted);
        }
        
        if (product is null)
        {
            response.Success = false;
            response.Message = "Product doesn't exist.";
        }
        else
        {
            response.Data = product;
        }

        return response;
    }

    public async Task<ServiceResponse<List<Book>>> GetBooksByCategory(string category)
    {
        var response = new ServiceResponse<List<Book>>
        {
            Data = await _dataContext.Books
                .Where( p => p.Category.Url.ToLower().Equals(category.ToLower()) && p.Visible && !p.Deleted)
                .Include( p => p.Variants.Where(v => v.Visible && !v.Deleted))
                .Include(p => p.Images)
                .ToListAsync()
        };

        return response;
    }

    public async Task<ServiceResponse<BookSearchDto>> SearchForBooks(string search, int page)
    {
        var numberOfResults = 2f;
        var pageCount = Math.Ceiling((await FindBySearchText(search)).Count / numberOfResults);

        var books = await _dataContext.Books
            .Where(p => p.Title.ToLower().Contains(search.ToLower()) ||
                        p.Description.ToLower().Contains(search.ToLower()) && 
                        p.Visible && !p.Deleted)
            .Include(p => p.Variants.Where(v => v.Visible && !v.Deleted))
            .Include(p => p.Images)
            .Skip((page - 1) * (int)numberOfResults)
            .Take((int)numberOfResults)
            .ToListAsync();
        
        var response = new ServiceResponse<BookSearchDto>
        {
            Data = new BookSearchDto
            {
                Books = books,
                Pages = (int)pageCount,
                CurrentPage = page
            }
        };

        return response;    
    }

    public async Task<ServiceResponse<List<string>>> GetBooksSearchSuggestions(string search)
    {
        var books = await FindBySearchText(search);
        
        List<string> bookTitles = new();
        foreach (var book in books)
        {
            if (book.Title.Contains(search, StringComparison.OrdinalIgnoreCase))
            {
                bookTitles.Add(book.Title);
            }

            if (book?.Description != null)
            {
                //search from description
                var punctuation = book.Description.Where(char.IsPunctuation).Distinct().ToArray(); //remove !.?,;. ....
                var words = book.Description.Split().Select(s => s.Trim(punctuation));

                foreach (var word in words)
                {
                    if (word.Contains(search, StringComparison.OrdinalIgnoreCase) && !bookTitles.Contains(word))
                    {
                        bookTitles.Add(word);
                    }
                }
            }
        }
        return new ServiceResponse<List<string>>
        {
            Data = bookTitles,
        };    
    }

    public async Task<ServiceResponse<List<FeaturedBookDto>>> GetFeaturedBooks()
    {
        var books = await _dataContext.Books
            .Where(p => p.Featured && p.Visible && !p.Deleted)
            .Include(p => p.Variants.Where(v => v.Visible && !v.Deleted))
            .Include(p => p.Images).Include(book => book.Author)
            .ToListAsync();

        var featuredBooks = books.Select(book => new FeaturedBookDto
            {
                Id = book.Id,
                ShortDescription = book.ShortDescription,
                Author = book.Author,
                Title = book.Title,
                Variants = book.Variants,
                DefaultImage = book.DefaultImage,
                Images = book.Images
            })
            .ToList();

        return new ServiceResponse<List<FeaturedBookDto>>
        {
            Data = featuredBooks
        };
    }

    public async Task<ServiceResponse<List<Book>>> GetAdminBooks()
    {
        var response = new ServiceResponse<List<Book>>
        {
            Data = await _dataContext.Books
                .Where(p => !p.Deleted)
                .Include(p => p.Variants.Where(v => !v.Deleted))
                .ThenInclude(pt => pt.BookType)
                .Include(p => p.Images)
                .ToListAsync()
        };

        return response;
    }

    public async Task<ServiceResponse<Book>> CreateBook(Book book)
    {
        foreach (var variant in book.Variants)
        {
            variant.BookType = null;
        }

        _dataContext.Books.Add(book);
        await _dataContext.SaveChangesAsync();
        return new ServiceResponse<Book> { Data = book };    
    }

    public async Task<ServiceResponse<Book>> UpdateBook(Book book)
    {
        var dbBook = await _dataContext.Books
            .Include(p => p.Images)
            .FirstOrDefaultAsync(p => p.Id == book.Id);
        
        if (dbBook == null)
        {
            return new ServiceResponse<Book>
            {
                Success = false,
                Message = "Product not found."
            };
        }

        dbBook.Title = book.Title;
        dbBook.Description = book.Description;
        dbBook.ShortDescription = book.ShortDescription;
        
        var bookImages = dbBook.Images;
        _dataContext.Images.RemoveRange(bookImages);
        dbBook.Images = book.Images;
        dbBook.DefaultImage = book.DefaultImage;
        dbBook.AuthorId = book.AuthorId;
        dbBook.CategoryId = book.CategoryId;
        dbBook.SeriesId = book.SeriesId;
        dbBook.SeriesOrder = book.SeriesOrder;
        dbBook.PageCount = book.PageCount;
        dbBook.DateAdded = DateTime.Now;
        dbBook.ReleaseDate = book.ReleaseDate;
        dbBook.Isbn = book.Isbn;
        dbBook.Featured = book.Featured;
        dbBook.CopiesInStore = dbBook.CopiesInStore;
        dbBook.Visible = book.Visible;

        
        foreach (var variant in book.Variants)
        {
            var dbVariant = await _dataContext.BookVariants
                .SingleOrDefaultAsync(v => v.BookId == variant.BookId && v.BookTypeId == variant.BookTypeId);
            if (dbVariant == null)
            {
                variant.BookType = null;
                _dataContext.BookVariants.Add(variant);
            }
            else
            {
                dbVariant.BookTypeId = variant.BookTypeId;
                dbVariant.Price = variant.Price;
                dbVariant.OriginalPrice = variant.OriginalPrice;
                dbVariant.Visible = variant.Visible;
                dbVariant.Deleted = variant.Deleted;
            }
        }

        await _dataContext.SaveChangesAsync();
        return new ServiceResponse<Book> { Data = book };
    }

    public async Task<ServiceResponse<bool>> DeleteBook(int id)
    {
        var dbProduct = await _dataContext.Books.FindAsync(id);
        if (dbProduct == null)
        {
            return new ServiceResponse<bool>
            {
                Data = false,
                Success = false,
                Message = "Product not found."
            };
        }

        dbProduct.Deleted = true;
        await _dataContext.SaveChangesAsync();
        return new ServiceResponse<bool> { Data = true };
    }
    
    private async Task<List<Book>> FindBySearchText(string search)
    {
        return await _dataContext.Books
            .Where(p => p.Title.ToLower().Contains(search.ToLower()) ||
                        p.Description.ToLower().Contains(search.ToLower()) && 
                        p.Visible && !p.Deleted)
            .Include(p => p.Variants.Where(v => v.Visible && !v.Deleted))
            .ToListAsync();
    }
}