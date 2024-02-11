using Ecommerce.Shared.Books;
using Ecommerce.Shared.DTOs;
using Ecommerce.Shared;
using Ecommerce.Shared.DTOs.Authors;
using Ecommerce.Shared.DTOs.Books;
using Ecommerce.Shared.DTOs.Series;

namespace Ecommerce.Server.Services.SeriesService
{
	public interface ISeriesService
	{
		Task<ServiceResponse<List<Series>>> GetSeries(int page, bool getAll);
		Task<ServiceResponse<SeriesDTO>> GetSingleSeries(string name);

		Task<ServiceResponse<Series>> AddSeries(Series category);
		Task<ServiceResponse<bool>> UpdateSeries(Series categoryDTO);
		Task<ServiceResponse<bool>> DeleteSeries(int categoryId);
		Task<ServiceResponse<Series>> GetSeriesById(int id);
		Task<ServiceResponse<Series>> GetSeriesByUrl(string url);
        Task<List<Series>> GetSeriesByAuthor(int authorId);


		Task<ServiceResponse<bool>> CreateSeries(EditSeriesModel editSeriesModel);
		Task<ServiceResponse<bool>> UpdateSeries(EditSeriesModel editSeriesModel);


		Task<int> GetSeriesCount();
		Task<ServiceResponse<List<DataSelectDTO>>> GetSeriesNames();


	}
}
