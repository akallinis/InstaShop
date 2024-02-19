
using MediatR;

namespace Instashop.Handlers.API.Products;

public class ApiProductDetailsRequest : IRequest<ApiProductDetailsResponse>
{
    public int ProductId { get; set; }
}