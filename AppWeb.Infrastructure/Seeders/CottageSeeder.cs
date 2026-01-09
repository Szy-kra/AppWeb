using AppWeb.Domain.Entities;
using AppWeb.Infrastructure.Persistence;

namespace AppWeb.Infrastructure.Seeders
{
    public class CottageSeeder
    {
        private readonly AppWebDbContext _dbContext;

        public CottageSeeder(AppWebDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Seed()
        {
            if (await _dbContext.Database.CanConnectAsync())
            {
                if (!_dbContext.Cottages.Any())
                {
                    var podTulipanem = new Cottage()
                    {
                        Name = "Pod Tulipanem",
                        // USUNĄŁEM Description stąd, bo powodował błąd CS0117
                        ContactDetails = new CottageDetails()
                        {
                            Description = "Niewielki domek położony w lesie, przy górskim potoku", // PRZENIESIONO TUTAJ
                            City = "Zakopane",
                            Street = "Leśna 8",
                            PostalCode = "34-500",
                            Price = 800.00m, // USUNIĘTO CUDZYSŁÓW I DODANO 'm' (decimal)
                            MaxPersons = 8
                        }
                    };

                    podTulipanem.EncodeName();
                    _dbContext.Cottages.Add(podTulipanem);
                    await _dbContext.SaveChangesAsync();
                }
            }
        }
    }
}