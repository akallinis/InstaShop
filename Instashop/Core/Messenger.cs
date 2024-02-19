using Instashop.Messages;

namespace Instashop.Core;

public static class Messenger
{
    public static event EventHandler<ProductListChangedMessage>? ProductListChanged;

    public static void SendProductListChangedMessage(ProductListChangedMessage message)
    {
        ProductListChanged?.Invoke(null, message);
    }
}
