using Instashop.MVVM.Models;
using Microsoft.EntityFrameworkCore;


namespace Instashop.Core;

public class InstaDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost; Database=Instashop; Username=*; Password=*");
    }

    public DbSet<Product>? Products { get; set; }
}