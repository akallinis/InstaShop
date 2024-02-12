using System.ComponentModel.DataAnnotations;

namespace Instashop.MVVM.Models;

public class Product
{
    [Key]
    public int Id { get; set; }
    public string? Name { get; set; }
    public double Price { get; set; }
}
