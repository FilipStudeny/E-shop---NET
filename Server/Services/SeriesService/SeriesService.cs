using Ecommerce.Server.Database;
using Ecommerce.Shared;
using Ecommerce.Shared.Books;
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
		public async Task<ServiceResponse<List<Series>>> GetSeries()
		{
			var series = await dataContext.Series.Where(series => series.Visible && !series.Deleted).ToListAsync();
			if(series == null)
			{
				return new ServiceResponse<List<Series>>
				{
					Success = false,
					Message = "No series found"
				};
			}

			return new ServiceResponse<List<Series>> { Data = series };
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
	}
}
