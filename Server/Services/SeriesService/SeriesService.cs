using Ecommerce.Server.Database;
using Ecommerce.Server.Services.AuthorsService;
using Ecommerce.Server.Services.BookService;
using Ecommerce.Shared;
using Ecommerce.Shared.Books;
using Ecommerce.Shared.DTOs;
using Ecommerce.Shared.DTOs.Authors;
using Ecommerce.Shared.DTOs.Books;
using Ecommerce.Shared.DTOs.Series;
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

		public async Task<ServiceResponse<List<DataSelectDTO>>> GetSeriesNames()
		{
			var seriesFromDb = await dataContext.Series.ToListAsync();
			if (seriesFromDb == null)
			{
				return new ServiceResponse<List<DataSelectDTO>>
				{
					Success = false,
					Message = "No books found."
				};
			}

			var series = seriesFromDb.Select(author => new DataSelectDTO
			{
				Id = author.Id,
				Name = author.Name
			}).ToList();


			return new ServiceResponse<List<DataSelectDTO>>
			{
				Data = series
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

			var books = await dataContext.Books
				.Where(book => book.SeriesId == series.Id && book.Visible && !book.Deleted)
				.Include(book => book.Images)
				.ToListAsync();

			foreach (var book in books)
			{
				if (book.Images != null && book.Images.Count > 0)
				{
					book.DefaultImageUrl = book.Images[0].Data;
				}
			}

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

		public async Task<ServiceResponse<bool>> CreateSeries(EditSeriesModel editSeriesModel)
		{
			try
			{
				var series = new Series
				{
					Name = editSeriesModel.Name,
					Description = editSeriesModel.Description,
					Url = editSeriesModel.Url,
					AuthorId = editSeriesModel.Author.Id,
					Visible = editSeriesModel.Visible,
					Deleted = false
				};

				dataContext.Series.Add(series);
				await dataContext.SaveChangesAsync();

				return new ServiceResponse<bool> { Data = true };
			}
			catch
			{
				return new ServiceResponse<bool> { Data = false, Success = false, Message = "Error, couldn't create new series" };
			}
		}

		public async Task<ServiceResponse<bool>> UpdateSeries(EditSeriesModel editSeriesModel)
		{
			var series = await dataContext.Series.FindAsync(editSeriesModel.Id);
			if(series == null)
			{
				return new ServiceResponse<bool> { Data = false, Success = false, Message = "Series not found" };
			}

			var author = await dataContext.Authors.FindAsync(editSeriesModel.Author.Id);

			series.Name = editSeriesModel.Name;
			series.Url = editSeriesModel.Url;
			series.Description = editSeriesModel.Description;
			series.Author = author;
			series.Visible = editSeriesModel.Visible;

			dataContext.Series.Update(series);
			await dataContext.SaveChangesAsync();

			return new ServiceResponse<bool> { Data = true, Message = "Series updated" };
		}
	}
}
