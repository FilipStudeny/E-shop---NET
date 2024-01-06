using Eshop.Server.Database;
using Eshop.Shared.Models.ProductModels;
using Microsoft.EntityFrameworkCore;

namespace Eshop.Server.Services.ProductTypeService;

public class ProductTypeService : IProductTypeService
{
    private readonly DataContext _dataContext;

    public ProductTypeService(DataContext dataContext)
    {
        this._dataContext = dataContext;
    }
    
    public async Task<ServiceResponse<List<ProductType>>> GetProductTypes()
    {
        var productTypes = await _dataContext.ProductTypes.ToListAsync();
        return new ServiceResponse<List<ProductType>> { Data = productTypes };
    }

    public async Task<ServiceResponse<List<ProductType>>> AddProductType(ProductType productType)
    {
        productType.Editing = false;
        productType.IsNew = false;
        _dataContext.ProductTypes.Add(productType);
        await _dataContext.SaveChangesAsync();
        return await GetProductTypes();
    }

    public async Task<ServiceResponse<List<ProductType>>> UpdateProductType(ProductType productType)
    {
        var dbProductType = await _dataContext.ProductTypes.FindAsync(productType.Id);
        if (dbProductType == null)
        {
            return new ServiceResponse<List<ProductType>>
            {
                Success = false,
                Message = "Product type not found"
            };
        }

        dbProductType.Name = productType.Name;
        await _dataContext.SaveChangesAsync();

        return await GetProductTypes();
    }
}