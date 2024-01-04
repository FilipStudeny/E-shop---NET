﻿using Eshop.Server.Services.Payment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eshop.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PaymentController : ControllerBase
{
    private readonly IPaymentService _paymentService;

    public PaymentController(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    [HttpPost]
    [Route("checkout"), Authorize]
    public async Task<ActionResult<string>> CreateCheckoutSession()
    {
        var response = await _paymentService.CreateCheckoutSession();
        return Ok(response.Url);
    }
    
    [HttpPost]
    public async Task<ActionResult<ServiceResponse<bool>>> FullfillOrder()
    {
        var response = await _paymentService.FulfillOrder(Request);
        if (!response.Success)
        {
            return BadRequest(response.Message);
        }
        return Ok(response);
    }
    
}