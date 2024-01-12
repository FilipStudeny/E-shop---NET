using Ecommerce.Server.Database;
using Ecommerce.Shared;
using Ecommerce.Shared.Books;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Server.Services.AuthorsService
{
	public class AuthorService : IAuthorsService
	{
		private readonly DataContext dataContext;

		public AuthorService(DataContext dataContext)
        {
			this.dataContext = dataContext;
		}


		public async Task<ServiceResponse<List<Author>>> GetAuthors(int page)
		{
			var authorsOnPage = 9;
			var authorCount = await dataContext.Authors.CountAsync(author => !author.Deleted && author.Visible);
			var pageCount = (int)Math.Ceiling((double)authorCount / authorsOnPage);

			var authors = await dataContext.Authors
				.Where(author => author.Visible && !author.Deleted)
				.Skip((page - 1) * authorsOnPage)
				.Take(authorsOnPage).ToListAsync();

			if(authors == null)
			{
				return new ServiceResponse<List<Author>>
				{
					Data = null,
					Success = false,
					Message = "No authors found."
				};
			}

			return new ServiceResponse<List<Author>> { 
				Data = authors,
				NumberOfPages = pageCount,
				CurrentPage = page
			};
		}

		public async Task<ServiceResponse<Author>> GetAuthor(string name)
		{
			var author = await dataContext.Authors.FirstOrDefaultAsync(author => author.Url == name && author.Visible && !author.Deleted);
			if (author == null)
			{
				return new ServiceResponse<Author>
				{
					Data = null,
					Success = false,
					Message = "No authors found."
				};
			}

			return new ServiceResponse<Author> { Data = author };
		}

		public Task<ServiceResponse<bool>> UpdateAuthor(Author author)
		{
			throw new NotImplementedException();
		}

		public Task<ServiceResponse<bool>> AddAuthor(Author author)
		{
			throw new NotImplementedException();
		}

		public Task<ServiceResponse<bool>> DeleteAuthor(int id)
		{
			throw new NotImplementedException();
		}
	}
}
