using Instashop.MVVM.Models;
using Shared.Responses.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instashop.Services.Interfaces;

public interface IProductsRepository
{
    IDataResponse GetProductsAll();
    IDataResponse SaveProducts(List<Product> products);
    IDataResponse UpdateProducts(List<Product> products);
}
