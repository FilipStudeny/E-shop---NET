﻿using Ecommerce.Server.Database;
using Ecommerce.Server.Services.BookService;
using Ecommerce.Server.Services.SeriesService;
using Ecommerce.Shared;
using Ecommerce.Shared.Books;
using Ecommerce.Shared.DTOs;
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
			var books = await dataContext.Books.Where(book => book.AuthorId == authorId && book.Visible && !book.Deleted).ToListAsync();


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
