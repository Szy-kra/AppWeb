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

            // KONFIGURACJA LOKALIZACJI I DETALI (ContactDetails_...)
            modelBuilder.Entity<Cottage>()
                .OwnsOne(c => c.ContactDetails, details =>
                {
                    details.Property(d => d.City).HasColumnName("ContactDetails_City");
                    details.Property(d => d.Street).HasColumnName("ContactDetails_Street");
                    details.Property(d => d.PostalCode).HasColumnName("ContactDetails_PostalCode");
                    details.Property(d => d.Description).HasColumnName("ContactDetails_Description");
                    details.Property(d => d.Price).HasColumnName("ContactDetails_Price").HasPrecision(18, 2);
                    details.Property(d => d.MaxPersons).HasColumnName("ContactDetails_MaxPersons");
                });

            // KONFIGURACJA RELACJI ZDJĘĆ
            modelBuilder.Entity<CottageImage>()
                .HasOne(ci => ci.Cottage)
                .WithMany(c => c.Images)
                .HasForeignKey(ci => ci.CottageId);
        }
    }
}