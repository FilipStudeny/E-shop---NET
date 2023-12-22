using Eshop.Server.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Eshop.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly DataContext dataContext;

    public ProductController(DataContext dataContext)
    {
        this.dataContext = dataContext;
    }

    [HttpGet]
    public async Task<ActionResult<List<Product>>> GetProduct()
    {
        var products = await dataContext.Products.ToListAsync();
        return Ok(products);
    }
}