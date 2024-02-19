using Instashop.MVVM.Models;
using MediatR;

namespace Instashop.Handlers.Repo.Products;

public class RepoUpdateProductsRequest : IRequest<RepoUpdateProductsResponse>
{
    public List<Product>? Products { get; set; }
}