using Instashop.MVVM.Models;
using Microsoft.EntityFrameworkCore;


namespace Instashop.Core;

public class InstaDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseNpgsql("Host=localhost; Database=Instashop; Username=postgres; Password=@Tf8g3jkp6y0v");
    }

    public DbSet<Product>? Products { get; set; }
}