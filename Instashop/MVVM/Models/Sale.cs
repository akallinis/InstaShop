namespace Instashop.MVVM.Models;

public class Sale
{
    public long SoldAt { get; set; }
    public List<SalesProduct>? Products { get; set; }
    public double TotalValue { get; set; }
}
