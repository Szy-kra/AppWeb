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
                .Include(c => c.Images)         // Klucz do zdjęć (slidera)
                .Include(c => c.ContactDetails) // Klucz do ceny, opisu i miasta!
                .ToListAsync();
        }

        // POPRAWIONE: Dodano brakującą metodę Update
        public async Task Update(Cottage cottage)
        {
            _dbContext.Cottages.Update(cottage);
            await _dbContext.SaveChangesAsync();
        }
    }
}