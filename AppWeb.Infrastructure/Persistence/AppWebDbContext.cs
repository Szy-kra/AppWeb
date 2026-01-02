using Microsoft.EntityFrameworkCore;

namespace AppWeb.Infrastructure.Persistence
{
    public class AppWebDbContext : DbContext
    {
        // Dodajemy konstruktor przyjmujący opcje, aby Program.cs mógł przekazać parametry z pliku JSON
        public AppWebDbContext(DbContextOptions<AppWebDbContext> options) : base(options)
        {
        }

        public DbSet<Domain.Entities.Cottage> Cottages { get; set; }

        // Metoda OnConfiguring została usunięta, ponieważ połączenie jest teraz zarządzane centralnie w Program.cs

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.Entities.Cottage>()
                 .OwnsOne(c => c.ContactDetails);
        }
    }
}