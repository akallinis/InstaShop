using Instashop.MVVM.Models;
using MediatR;

namespace Instashop.Handlers.Repo.Products;

public class RepoSaveProductsRequest : IRequest<RepoSaveProductsResponse>
{
    public List<Product>? Products { get; set; }
}
