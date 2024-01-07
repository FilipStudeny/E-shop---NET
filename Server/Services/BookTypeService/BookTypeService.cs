using Eshop.Server.Database;
using Eshop.Shared.Models.Books;
using Microsoft.EntityFrameworkCore;

namespace Eshop.Server.Services.BookTypeService;

public class BookTypeService : IBookTypeService
{
    private readonly DataContext _dataContext;

    public BookTypeService(DataContext dataContext)
    {
        _dataContext = dataContext;
    }
    
    
    public async Task<ServiceResponse<List<BookType>>> GetBookTypes()
    {
        var bookTypes = await _dataContext.BookTypes.ToListAsync();
        var response = new ServiceResponse<List<BookType>> { Data = bookTypes };
        return response;
    }

    public async Task<ServiceResponse<List<BookType>>> AddBookTypes(BookType bookType)
    {
        bookType.Editing = false;
        bookType.IsNew = false;
        _dataContext.BookTypes.Add(bookType);
        await _dataContext.SaveChangesAsync();
        return await GetBookTypes();    
    }

    public async Task<ServiceResponse<List<BookType>>> UpdateBookType(BookType bookType)
    {
        var dbProductType = await _dataContext.BookTypes.FindAsync(bookType.Id);
        if (dbProductType == null)
        {
            return new ServiceResponse<List<BookType>>
            {
                Success = false,
                Message = "Book type not found"
            };
        }

        dbProductType.Name = bookType.Name;
        await _dataContext.SaveChangesAsync();

        return await GetBookTypes();
    }
}