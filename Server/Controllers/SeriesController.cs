using Ecommerce.Server.Services.CategoryService;
using Ecommerce.Shared.Books;
using Ecommerce.Shared;
using Microsoft.AspNetCore.Mvc;
using Ecommerce.Server.Services.SeriesService;
using Ecommerce.Client.Services.AuthorsService;
using Ecommerce.Shared.DTOs.Authors;
using Ecommerce.Shared.DTOs.Series;
using Microsoft.AspNetCore.Authorization;

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
		[Route("{page}")]
		public async Task<ActionResult<ServiceResponse<List<Series>>>> GetSeries(int page)
		{
			var response = await seriesService.GetSeries(page, false);
			return Ok(response);
		}

		[HttpGet]
		[Route("admin/{page}")]
		public async Task<ActionResult<ServiceResponse<List<Series>>>> GetAllSeries(int page)
		{
			var response = await seriesService.GetSeries(page, true);
			return Ok(response);
		}

		[HttpGet]
		[Route("series/{name}")]
		public async Task<ActionResult<ServiceResponse<List<Series>>>> GetSeries(string name)
		{
			var response = await seriesService.GetSingleSeries(name);
			return Ok(response);
		}


		//**EDITS**//
		[HttpGet]
		[Route("admin/all"), Authorize]
		public async Task<ActionResult<ServiceResponse<List<DataSelectDTO>>>> GetAllSeriesNames()
		{
			var response = await seriesService.GetSeriesNames();
			return Ok(response);
		}

		[HttpPut]
		[Route("admin/update"), Authorize]
		public async Task<ActionResult<ServiceResponse<bool>>> UpdateSeries(EditSeriesModel editSeriesModel)
		{
			var response = await seriesService.UpdateSeries(editSeriesModel);
			return Ok(response);
		}

		[HttpPost]
		[Route("admin/add"), Authorize]
		public async Task<ActionResult<ServiceResponse<bool>>> CreateSeries(EditSeriesModel editSeriesModel)
		{
			var response = await seriesService.CreateSeries(editSeriesModel);
			return Ok(response);
		}
	}
}
