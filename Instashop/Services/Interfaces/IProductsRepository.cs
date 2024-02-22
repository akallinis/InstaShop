using Instashop.MVVM.Models;
using Shared.Responses.Interfaces;

namespace Instashop.Services.Interfaces;

public interface IProductsRepository
{
    IDataResponse GetProductsAll();
    IDataResponse SaveProducts(List<Product> products);
    IDataResponse UpdateProducts(List<Product> products);
}
