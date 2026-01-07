using Microsoft.EntityFrameworkCore;

namespace AppWeb.Infrastructure.Persistence
{
    public class AppWebDbv2Context : DbContext
    {
        // Dodajemy konstruktor przyjmujący opcje, aby Program.cs mógł przekazać parametry z pliku JSON
        public AppWebDbv2Context(DbContextOptions<AppWebDbv2Context> options) : base(options)
        {
        }

        //właściwości DbSet która REPREZENTUJE tabelę w bazie danych
        public DbSet<Domain.Entities.Cottage> Cottages { get; set; }

        public DbSet<Domain.Entities.CottageImage> CottageImages { get; set; }




        //własciwosc w ramach tabeli ktora jest reprezentacja encji w Cottage
        //relacja miedzy Cottage a ContactDetails
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.Entities.Cottage>()
             .OwnsOne(c => c.ContactDetails);
        }

    }
}