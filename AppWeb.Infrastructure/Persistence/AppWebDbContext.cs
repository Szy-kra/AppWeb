using AppWeb.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AppWeb.Infrastructure.Persistence
{
    public class AppWebDbContext : DbContext
    {
        public AppWebDbContext(DbContextOptions<AppWebDbContext> options) : base(options)
        {
        }

        public DbSet<Cottage> Cottages { get; set; }
        public DbSet<CottageImage> CottageImages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Konfiguracja pod nową bazę v3
            modelBuilder.Entity<Cottage>(eb =>
            {
                // Łączymy detale w jedną tabelę
                eb.OwnsOne(c => c.ContactDetails, details =>
                {
                    details.Property(d => d.Price).HasPrecision(18, 2);
                });
            });
        }
    }
}