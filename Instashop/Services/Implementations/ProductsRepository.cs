using Instashop.Core;
using Instashop.MVVM.Models;
using Instashop.Services.Interfaces;
using Shared.Responses.Factories;
using Shared.Responses.Interfaces;

namespace Instashop.Services.Implementations;

public class ProductsRepository : IProductsRepository
{
    private readonly InstaDbContext _dbContext;
    public ProductsRepository(InstaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IDataResponse GetProductsAll()
    {
        try
        {
            var data = _dbContext.Products?.Select(p => new Product
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price
            }).ToList();

            return data.Any() ? DataResponseFactory.CreateDataResult(data) : DataResponseFactory.CreateNoDataResult();
        }
        catch (Exception ex)
        {
            return DataResponseFactory.CreateExceptionResult(ex);
        }
    }

    public IDataResponse SaveProducts(List<Product> products)
    {
        try
        {
            if (products != null)
            {
                _dbContext.Products?.AddRange(products);

                _dbContext.SaveChanges();
                return DataResponseFactory.CreateSuccessResult();
            }

            return DataResponseFactory.CreateFailResult();
        }
        catch (Exception ex)
        {
            return DataResponseFactory.CreateExceptionResult(ex);
        }
    }

    public IDataResponse UpdateProducts(List<Product> products)
    {
        try
        {
            if (products != null)
            {
                _dbContext.Products?.UpdateRange(products);
                _dbContext.SaveChanges();
                return DataResponseFactory.CreateSuccessResult();
            }

            return DataResponseFactory.CreateFailResult();
        }
        catch (Exception ex)
        {
            return DataResponseFactory.CreateExceptionResult(ex);
        }
    }
}
