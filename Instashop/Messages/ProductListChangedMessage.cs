using Instashop.MVVM.Models;

namespace Instashop.Messages;

public class ProductListChangedMessage
{
    public List<Product>? Products { get; set; }
}
