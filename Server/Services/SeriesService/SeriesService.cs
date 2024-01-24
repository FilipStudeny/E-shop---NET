using Ecommerce.Server.Database;
using Ecommerce.Server.Services.AuthorsService;
using Ecommerce.Server.Services.BookService;
using Ecommerce.Shared;
using Ecommerce.Shared.Books;
using Ecommerce.Shared.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Server.Services.SeriesService
{
	public class SeriesService : ISeriesService
	{
		private readonly DataContext dataContext;

		public SeriesService(DataContext dataContext)
        {
			this.dataContext = dataContext;
		}

		public async Task<ServiceResponse<List<Series>>> GetSeries(int page, bool getAll = false)
		{
			var seriesOnPage = 20;
			var seriesCount = getAll == false ?
				await dataContext.Series.CountAsync(series => !series.Deleted && series.Visible) :
				await dataContext.Series.CountAsync();

			var pageCount = (int)Math.Ceiling((double)seriesCount / seriesOnPage);

			var series = await dataContext.Series
					.Where(series => series.Visible && !series.Deleted)
					.Skip((page - 1) * seriesOnPage)
					.Take(seriesOnPage)
					.ToListAsync();

			if(series == null)
			{
				return new ServiceResponse<List<Series>>
				{
					Success = false,
					Message = "No series found"
				};
			}

			return new ServiceResponse<List<Series>> { 
				Data = series,
				CurrentPage = page,
				NumberOfPages = pageCount,
				ItemCount = seriesCount
				
			};
		}

		public async Task<ServiceResponse<SeriesDTO>> GetSingleSeries(string name)
		{
			var series = await dataContext.Series.FirstOrDefaultAsync(series => series.Url == name && series.Visible && !series.Deleted);
			if(series == null)
			{
				return new ServiceResponse<SeriesDTO>
				{
					Data = null,
					Success = false,
					Message = "No series found."
				};
			}

			var books = await dataContext.Books.Where(book => book.SeriesId == series.Id && book.Visible && !book.Deleted).ToListAsync();
            var author = await dataContext.Authors.FirstOrDefaultAsync(author => author.Id == series.AuthorId && author.Visible && !author.Deleted);

            return new ServiceResponse<SeriesDTO>
			{
				Data = new SeriesDTO
				{
					Series = series,
					Author = author,
					Books = books
				}
			}; 
		}

        public Task<ServiceResponse<Series>> AddSeries(Series category)
		{
			throw new NotImplementedException();
		}

		public Task<ServiceResponse<bool>> DeleteSeries(int categoryId)
		{
			throw new NotImplementedException();
		}

		public async Task<ServiceResponse<Series>> GetSeriesById(int id)
		{
			var series = await dataContext.Series.FirstOrDefaultAsync(series => series.Id == id && series.Visible && !series.Deleted);
			if (series == null)
			{
				return new ServiceResponse<Series>
				{
					Success = false,
					Message = "No series found"
				};
			}

			return new ServiceResponse<Series> { Data = series };
		}

		public async Task<ServiceResponse<Series>> GetSeriesByUrl(string url)
		{
			var series = await dataContext.Series.FirstOrDefaultAsync(series => series.Url == url && series.Visible && !series.Deleted);
			if (series == null)
			{
				return new ServiceResponse<Series>
				{
					Success = false,
					Message = "No series found"
				};
			}

			return new ServiceResponse<Series> { Data = series };
		}

		public Task<ServiceResponse<bool>> UpdateSeries(Series categoryDTO)
		{
			throw new NotImplementedException();
		}

		public async Task<int> GetSeriesCount()
		{
			return await dataContext.Series.CountAsync(series => !series.Deleted && series.Visible);

		}

        public async Task<List<Series>> GetSeriesByAuthor(int authorId)
        {
			var series = await dataContext.Series.Where(series => series.AuthorId == authorId && series.Visible && !series.Deleted).ToListAsync();
			if(series == null)
			{
				return new();
			}

			return series;
        }

		
	}
}
