using Eshop.Shared.Models.Books;

namespace Eshop.Server.Services.AuthorService;

public interface IAuthorService
{
    Task<ServiceResponse<List<Author>>> GetAuthors();
    Task<ServiceResponse<Author>> GetAuthorById(int id);
    Task<ServiceResponse<Author>> AddAuthor(Author author);
    Task<ServiceResponse<Author>> UpdateAuthor(Author author);
    Task<ServiceResponse<bool>> DeleteAuthor(int id);
}