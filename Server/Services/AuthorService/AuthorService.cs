using Eshop.Server.Database;
using Eshop.Shared.Models.Books;
using Microsoft.EntityFrameworkCore;

namespace Eshop.Server.Services.AuthorService;

public class AuthorService : IAuthorService
{
    private readonly DataContext _dataContext;

    public AuthorService(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<ServiceResponse<List<Author>>> GetAuthors()
    {
        var authors = await _dataContext.Authors.ToListAsync();
        return new ServiceResponse<List<Author>> { Data = authors };
    }

    public async Task<ServiceResponse<Author>> GetAuthorById(int id)
    {
        var author = await _dataContext.Authors.FirstOrDefaultAsync(a => a.Id == id);
        if (author == null)
        {
            return new ServiceResponse<Author>
            {
                Success = false,
                Message = $"Author with an Id of {id} not found"
            };
        }
        return new ServiceResponse<Author> { Data = author };    
    }

    public async Task<ServiceResponse<Author>> AddAuthor(Author author)
    {
        var authorInDb = await FindAuthor(author);
        if (authorInDb != null)
        {
            return new ServiceResponse<Author>
            {
                Data = authorInDb,
                Success = false,
                Message = "Couldn't add new author, Author already exists"
            };
        }

        _dataContext.Authors.Add(author);
        await _dataContext.SaveChangesAsync();
        return new ServiceResponse<Author> { Data = author };
    }

    public async Task<ServiceResponse<Author>> UpdateAuthor(Author author)
    {
        var authorInDb = await FindAuthor(author);
        if (authorInDb == null)
        {
           return await AddAuthor(author);
        }

        authorInDb.Name = author.Name;
        authorInDb.BiographyText = author.BiographyText;
        await _dataContext.SaveChangesAsync();
        return new ServiceResponse<Author>{ Data = authorInDb};
    }

    public Task<ServiceResponse<bool>> DeleteAuthor(int id)
    {
        throw new NotImplementedException();
    }

    private async Task<Author?> FindAuthor(Author author)
    {
        var authorInDb = await _dataContext.Authors
            .FirstOrDefaultAsync(a => a.Id == author.Id && a.Name == author.Name);
        return authorInDb;
    }

}