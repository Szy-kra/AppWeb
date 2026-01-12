using AppWeb.Domain.Entities;
using AppWeb.Domain.Interfaces;
using AppWeb.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AppWeb.Infrastructure.Repositories
{
    public class CottageRepository : ICottageRepository
    {
        private readonly AppWebDbContext _dbContext;

        public CottageRepository(AppWebDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Create(Cottage cottage)
        {
            _dbContext.Add(cottage);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Cottage>> GetAllCottage()
        {
            return await _dbContext.Cottages
                .Include(c => c.Images)
                .Include(c => c.ContactDetails)
                .ToListAsync();
        }

        // 1. Metoda do pobierania jednego domku (Niezbędna do edycji!)
        public async Task<Cottage?> GetByEncodedName(string encodedName)
        {
            return await _dbContext.Cottages
                .Include(c => c.ContactDetails)
                .Include(c => c.Images)
                .FirstOrDefaultAsync(c => c.EncodedName == encodedName);
        }

        // 2. Metoda do aktualizacji całego obiektu
        public async Task Update(Cottage cottage)
        {
            _dbContext.Cottages.Update(cottage);
            await _dbContext.SaveChangesAsync();
        }

        // 3. Metoda Commit (Zatwierdzanie zmian śledzonych przez EF)
        public async Task Commit()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}