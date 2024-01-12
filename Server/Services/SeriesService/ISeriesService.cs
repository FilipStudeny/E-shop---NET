using Ecommerce.Shared.Books;
using Ecommerce.Shared.DTOs;
using Ecommerce.Shared;

namespace Ecommerce.Server.Services.SeriesService
{
	public interface ISeriesService
	{
		Task<ServiceResponse<List<Series>>> GetSeries();
		Task<ServiceResponse<Series>> AddSeries(Series category);
		Task<ServiceResponse<bool>> UpdateSeries(Series categoryDTO);
		Task<ServiceResponse<bool>> DeleteSeries(int categoryId);
		Task<ServiceResponse<Series>> GetSeriesById(int id);
		Task<ServiceResponse<Series>> GetSeriesByUrl(string url);

		Task<int> GetSeriesCount();


	}
}
