using AppWeb.Domain.Entities; // Powiązanie z modelami danych (Cottage.cs)
using AppWeb.Infrastructure.Persistence; // Powiązanie z bazą danych (AppWebDbv2Context.cs)

namespace AppWeb.Infrastructure.Seeders
{
    public class CottageSeeder
    {
        private readonly AppWebDbv2Context _dbContext;

        // Konstruktor wstrzykujący narzędzie do obsługi bazy (zdefiniowane w Persistence/AppWebDbv2Context.cs)
        public CottageSeeder(AppWebDbv2Context dbContext)
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
                        ContactDetails = new CottageDetails() // Powiązanie z Domain/Entities/CottageDetails.cs
                        {
                            City = "Zakopane",
                            Street = "Leśna 8",
                            PostalCode = "34-500",
                            Price = "800",
                            MaxPersons = 8
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