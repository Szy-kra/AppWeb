using Microsoft.EntityFrameworkCore;

namespace AppWeb.Infrastructure.Persistence
{
    public class AppWebDbContext : DbContext
    {
        public DbSet<Domain.Entities.Cottage> Cottages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=AppWebDb;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.Entities.Cottage>()
                 .OwnsOne(c => c.ContactDetails);

        }

    }
}
