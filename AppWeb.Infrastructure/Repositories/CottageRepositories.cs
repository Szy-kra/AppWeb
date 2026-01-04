using AppWeb.Domain.Interfaces;
using AppWeb.Infrastructure.Persistence;




namespace AppWeb.Infrastructure.Repositories
{
    internal class CottageRepositories : ICottageRepository
    {
        private readonly AppWebDbContext _dbContext;

        public CottageRepositories(AppWebDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Create(Domain.Entities.Cottage cottage) // Dodaj 'async'
        {
            _dbContext.Add(cottage); // Dodajemy do kolejki
            await _dbContext.SaveChangesAsync(); // Wysyłamy do SQL
        }
    }
}