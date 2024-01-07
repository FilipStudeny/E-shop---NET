using Eshop.Server.Database;
using Eshop.Server.Services.ProductService;
using Eshop.Shared.DTOs;
using Eshop.Shared.Models;
using Eshop.Shared.Models.ProductModels;
using Microsoft.AspNetCore.Authorization;
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
    [Route("search/{search}/{page}")]
    public async Task<ActionResult<ServiceResponse<ProductSearchDto>>> SearchForProduct(string search, int page = 1)
    {
        var products = await _productService.SearchProducts(search, page);
        return Ok(products);
    }
    
    [HttpGet]
    [Route("suggestions/{search}")]
    public async Task<ActionResult<ServiceResponse<Product>>> SearchProductSuggestions(string search)
    {
        var products = await _productService.GetProductSearchSuggestions(search);
        return Ok(products);
    }
    
    [HttpGet]
    [Route("featured")]
    public async Task<ActionResult<ServiceResponse<Product>>> GetFeaturedProducts()
    {
        var products = await _productService.GetFeaturedProducts();
        return Ok(products);
    }
    
    [HttpGet]
    [Route("admin"), Authorize(Roles = "Admin")]
    public async Task<ActionResult<ServiceResponse<List<Product>>>> GetAdminProducts()
    {
        var products = await _productService.GetAdminProducts();
        return Ok(products);
    }
    
    [HttpPost]
    [Route("admin"), Authorize(Roles = "Admin")]
    public async Task<ActionResult<ServiceResponse<Product>>> CreateProduct(Product product)
    {
        var products = await _productService.CreateProduct(product);
        return Ok(products);
    }
    
    [HttpPut]
    [Route("admin"), Authorize(Roles = "Admin")]
    public async Task<ActionResult<ServiceResponse<Product>>> UpdateProduct(Product product)
    {
        var products = await _productService.UpdateProduct(product);
        return Ok(products);
    }
    
    [HttpDelete]
    [Route("admin/{id}"), Authorize(Roles = "Admin")]
    public async Task<ActionResult<ServiceResponse<bool>>> DeleteProduct(int id)
    {
        var products = await _productService.DeleteProduct(id);
        return Ok(products);
    }

}