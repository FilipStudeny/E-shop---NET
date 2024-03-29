﻿using Ecommerce.Server.Services.AuthorsService;
using Ecommerce.Shared;
using Ecommerce.Shared.Books;
using Ecommerce.Shared.DTOs;
using Ecommerce.Shared.DTOs.Authors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Server.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthorsController : ControllerBase
	{
		private readonly IAuthorsService authorsService;

		public AuthorsController(IAuthorsService authorsService)
        {
			this.authorsService = authorsService;
		}

		[HttpGet]
		[Route("{page}")]
		public async Task<ActionResult<ServiceResponse<List<Author>>>> GetAuthors(int page)
		{
			var response = await authorsService.GetAuthors(page);
			return Ok(response);
		}

		[HttpGet]
		[Route("admin/{page}")]
		public async Task<ActionResult<ServiceResponse<List<Author>>>> GetAllAuthors(int page)
		{
			var response = await authorsService.GetAuthors(page, true);
			return Ok(response);
		}

		[HttpGet]
		[Route("author/{name}")]
		public async Task<ActionResult<ServiceResponse<AuthorDTO>>> GetAuthor(string name)
		{
			var response = await authorsService.GetAuthor(name);
			return Ok(response);
		}




		//**EDITS**//
		[HttpGet]
		[Route("admin/all")]
		public async Task<ActionResult<ServiceResponse<List<DataSelectDTO>>>> GetAllAuthorNames()
		{
			var response = await authorsService.GetAuthorsForEdit();
			return Ok(response);
		}

		[HttpGet]
		[Route("admin/author/{name}"), Authorize]
		public async Task<ActionResult<ServiceResponse<EditAuthorModel>>> GetAuthorForEdit(string name)
		{
			var response = await authorsService.GetAuthorForEdit(name);
			return Ok(response);
		}

		[HttpPut]
		[Route("admin/author/update"), Authorize]
		public async Task<ActionResult<ServiceResponse<bool>>> UpdateAuthor(EditAuthorModel authorModel)
		{
			var response = await authorsService.UpdateAuthor(authorModel);
			return Ok(response);
		}

		[HttpPost]
		[Route("admin/author/add"), Authorize]
		public async Task<ActionResult<ServiceResponse<bool>>> AddAuthor(EditAuthorModel editAuthorModel)
		{
			var response = await authorsService.CreateAuthor(editAuthorModel);
			return Ok(response);
		}
	}
}
