



using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OrderProcessing.OrderService.Domain.Common;
using OrderProcessing.OrderService.Domain.Entities;

namespace OrderProcessing.OrderService.AppDataContext;


public class OrderDbContext : DbContext
{


    private readonly DbSettings _dbSettings;
    public OrderDbContext(IOptions<DbSettings> dbSettings)
    {
        _dbSettings = dbSettings.Value;
    }


    public DbSet<Domain.Entities.Order> Orders { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_dbSettings.ConnectionString);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Domain.Entities.Order>()
            .HasKey(o => o.OrderId);


        modelBuilder.Entity<Domain.Entities.OrderItem>()
            .HasKey(oi => oi.ProductId);


    }
}