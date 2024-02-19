using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Instashop.MVVM.Models;

public class Product
{
    [Key]
    public int Id { get; set; }
    public string? Name { get; set; }
    public double Price { get; set; }

    public override bool Equals(object? obj)
    {
        if (obj == null) return false;

        if (!ReferenceEquals(this, obj)) return false;

        if (obj.GetType() != this.GetType()) return false;

        var other = obj as Product;

        return this.Id == other.Id &&
            this.Name == other.Name &&
            this.Price == other.Price;
    }
}

public class ProductComparer : IEqualityComparer<Product>
{
    public bool Equals(Product? x, Product? y)
    {
        return x.Id == y.Id;
    }

    public int GetHashCode([DisallowNull] Product obj)
    {
        return obj.Id.GetHashCode();
    }
}
