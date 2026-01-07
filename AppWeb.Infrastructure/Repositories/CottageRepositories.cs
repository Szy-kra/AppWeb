using AppWeb.Domain.Entities;
using AppWeb.Domain.Interfaces;
using AppWeb.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AppWeb.Infrastructure.Repositories
{
    public class CottageRepository : ICottageRepository
    {
        private readonly AppWebDbv2Context _dbContext;

        public CottageRepository(AppWebDbv2Context dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Create(Cottage cottage)
        {
            _dbContext.Add(cottage);
            await _dbContext.SaveChangesAsync();
        }

        // TO DODAJEMY (Przyprawy do slidera):
        public async Task<IEnumerable<Cottage>> GetAll()
        {
            return await _dbContext.Cottages
                .Include(c => c.Images) // <--- Klucz do slidera!
                .ToListAsync();
        }
    }
}