using Ecommerce.Server.Database;
using Ecommerce.Server.Services.BookService;
using Ecommerce.Server.Services.SeriesService;
using Ecommerce.Shared;
using Ecommerce.Shared.Books;
using Ecommerce.Shared.DTOs;
using Ecommerce.Shared.DTOs.Authors;
using Ecommerce.Shared.DTOs.Books;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace Ecommerce.Server.Services.AuthorsService
{
	public class AuthorService : IAuthorsService
	{
		private readonly DataContext dataContext;
        private readonly ISeriesService seriesService;

        public AuthorService(DataContext dataContext, ISeriesService seriesService)
        {
			this.dataContext = dataContext;
            this.seriesService = seriesService;
        }


		public async Task<ServiceResponse<List<Author>>> GetAuthors(int page, bool getAll = false)
		{
			var authorsOnPage = 9;
			var authorCount = getAll == false ?
				await dataContext.Authors.CountAsync(author => !author.Deleted && author.Visible) :
				await dataContext.Authors.CountAsync();

			var pageCount = (int)Math.Ceiling((double)authorCount / authorsOnPage);

			var authors = getAll == false ?
				await dataContext.Authors
					.Where(author => author.Visible && !author.Deleted)
					.Skip((page - 1) * authorsOnPage)
					.Take(authorsOnPage).ToListAsync() :
				await dataContext.Authors
					.Skip((page - 1) * authorsOnPage)
					.Take(authorsOnPage).ToListAsync();

			if (authors == null)
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
				CurrentPage = page,
				ItemCount = authorCount
			};
		}


		public async Task<ServiceResponse<List<DataSelectDTO>>> GetAuthorsForEdit()
		{
			var authorsFromDb = await dataContext.Authors.ToListAsync();
			if (authorsFromDb == null)
			{
				return new ServiceResponse<List<DataSelectDTO>>
				{
					Success = false,
					Message = "No books found."
				};
			}

			var authors = authorsFromDb.Select(author => new DataSelectDTO
			{
				Id = author.Id,
				Name = author.Name
			}).ToList();


			return new ServiceResponse<List<DataSelectDTO>>
			{
				Data = authors
			};
		}
		public async Task<ServiceResponse<AuthorDTO>> GetAuthor(string name)
		{
			var author = await dataContext.Authors.FirstOrDefaultAsync(author => author.Url == name && author.Visible && !author.Deleted);
			if (author == null)
			{
				return new ServiceResponse<AuthorDTO>
				{
					Data = null,
					Success = false,
					Message = "No authors found."
				};
			}

            var authorId = author.Id;
			var series = await seriesService.GetSeriesByAuthor(authorId);
			var books = await dataContext.Books
				.Where(book => book.AuthorId == authorId && book.Visible && !book.Deleted)
				.Include(book => book.Images)
				.ToListAsync();

			foreach (var book in books)
			{
				if (book.Images != null && book.Images.Count > 0)
				{
					book.DefaultImageUrl = book.Images[0].Data;
				}
			}

			var data = new AuthorDTO
			{
				Author = author,
				Series = series,
				Books = books
			};

            return new ServiceResponse<AuthorDTO> { Data = data };
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
