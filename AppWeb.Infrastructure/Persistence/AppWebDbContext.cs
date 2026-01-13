using AppWeb.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AppWeb.Infrastructure.Persistence
{
    // Dziedziczymy po IdentityDbContext, tak jak na Twoim obrazku pomocniczym
    public class AppWebDbContext : IdentityDbContext
    {
        public AppWebDbContext(DbContextOptions<AppWebDbContext> options) : base(options)
        {
        }

        public DbSet<Cottage> Cottages { get; set; }
        public DbSet<CottageImage> CottageImages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // TO JEST NAJWAŻNIEJSZE (zaznaczone czerwoną strzałką na Twoim foto)
            // base.OnModelCreating musi być pierwsze!
            base.OnModelCreating(modelBuilder);

            // Twoja konfiguracja ContactDetails
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

            // Twoja konfiguracja zdjęć
            modelBuilder.Entity<CottageImage>()
                .HasOne(ci => ci.Cottage)
                .WithMany(c => c.Images)
                .HasForeignKey(ci => ci.CottageId);
        }
    }
}