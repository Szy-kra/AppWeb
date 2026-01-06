using AppWeb.Domain.Entities; // Powiązanie z modelami danych (Cottage.cs)
using AppWeb.Infrastructure.Persistence; // Powiązanie z bazą danych (AppWebDbContext.cs)

namespace AppWeb.Infrastructure.Seeders
{
    public class CottageSeeder
    {
        private readonly AppWebDbContext _dbContext;

        // Konstruktor wstrzykujący narzędzie do obsługi bazy (zdefiniowane w Persistence/AppWebDbContext.cs)
        public CottageSeeder(AppWebDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Seed()
        {
            // Sprawdza połączenie z serwerem SQL (konfiguracja w appsettings.json)
            if (await _dbContext.Database.CanConnectAsync())
            {
                // Sprawdza, czy tabela Cottages w bazie danych jest pusta
                if (!_dbContext.Cottages.Any())
                {
                    // Tworzenie nowej instancji obiektu na podstawie klasy z Domain/Entities/Cottage.cs
                    var podTulipanem = new Cottage()
                    {
                        Name = "Pod Tulipanem",
                        Description = "Niewielki domek położony w lesie, przy górskim potoku",
                        ContactDetails = new CottageContactDetails() // Powiązanie z Domain/Entities/CottageContactDetails.cs
                        {
                            City = "Zakopane",
                            Street = "Leśna 5",
                            PostalCode = "34-500",
                            Price = "300",
                            Email = "-"
                        }
                    };

                    // Wywołanie metody z pliku Cottage.cs, która przygotowuje nazwę pod adres URL (np. "pod-tulipanem")
                    podTulipanem.EncodeName();

                    // Dodanie obiektu do lokalnej kolekcji (jeszcze nie wysyła do bazy)
                    _dbContext.Cottages.Add(podTulipanem);

                    // Fizyczne wysłanie danych i zapisanie ich jako rekord w tabeli SQL
                    await _dbContext.SaveChangesAsync();
                }
            }
        }
    }
}