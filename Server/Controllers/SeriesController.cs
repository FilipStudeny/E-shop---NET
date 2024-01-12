using Ecommerce.Server.Services.CategoryService;
using Ecommerce.Shared.Books;
using Ecommerce.Shared;
using Microsoft.AspNetCore.Mvc;
using Ecommerce.Server.Services.SeriesService;

namespace Ecommerce.Server.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SeriesController : ControllerBase
	{
		private readonly ISeriesService seriesService;

		public SeriesController(ISeriesService seriesService)
        {
			this.seriesService = seriesService;
		}

        [HttpGet]
		public async Task<ActionResult<ServiceResponse<List<Series>>>> GetSeries()
		{
			var response = await seriesService.GetSeries();
			return Ok(response);
		}
	}
}
