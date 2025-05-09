using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OrderProcessing.ProductService.Domain.Common;

namespace OrderProcessing.ProductService.AppDbContext;



public class ProductDbContext : DbContext
{


    private readonly DbSettings _dbSettings;
    public ProductDbContext(IOptions<DbSettings> dbSettings)
    {
        _dbSettings = dbSettings.Value;
    }


    public DbSet<Domain.Entities.Product> Product { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_dbSettings.ConnectionString);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Domain.Entities.Product>()
            .HasKey(o => o.Id);

         modelBuilder.Entity<Domain.Entities.Product>()
        .Property(p => p.Price)
        .HasPrecision(18, 2);

    }
}