using Eshop.Server.Database;
using Eshop.Server.Services.ProductService;
using Eshop.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Eshop.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    
    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<ActionResult<ServiceResponse<List<Product>>>> GetProducts()
    {
        var products = await _productService.GetProducts();
        return Ok(products);
    }
    
    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult<ServiceResponse<Product>>> GetProduct(int id)
    {
        var product = await _productService.GetProduct(id);
        return Ok(product);
    }
    
    [HttpGet]
    [Route("category/{category}")]
    public async Task<ActionResult<ServiceResponse<Product>>> GetProductsByCategory(string category)
    {
        var products = await _productService.GetProductsByCategory(category);
        return Ok(products);
    }
    
    [HttpGet]
    [Route("search/{search}")]
    public async Task<ActionResult<ServiceResponse<Product>>> SearchForProduct(string search)
    {
        var products = await _productService.SearchProducts(search);
        return Ok(products);
    }
    
    [HttpGet]
    [Route("suggestions/{search}")]
    public async Task<ActionResult<ServiceResponse<Product>>> SearchProductSuggestions(string search)
    {
        var products = await _productService.GetProductSearchSuggestions(search);
        return Ok(products);
    }

}