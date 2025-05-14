




using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OrderProcessing.Domain.Models;

namespace OrderProcessing.InventoryService.AppDbContext
{
   public class InventoryDbContext : DbContext
    {
        private readonly DbSettings _dbSettings;

        public InventoryDbContext(IOptions<DbSettings> dbSettings)
        {
            _dbSettings = dbSettings.Value;
        }

        public DbSet<Domain.Entities.ReservationItem> ReservationItems { get; set; }


        public DbSet<Domain.Entities.Reservation> Reservations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_dbSettings.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.Entities.ReservationItem>()
                .HasKey(o => o.Id);

            modelBuilder.Entity<Domain.Entities.ReservationItem>()
                .HasKey(p => p.Id);
               
        }
    }
    
}