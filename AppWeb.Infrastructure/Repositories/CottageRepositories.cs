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

        public async Task<Cottage?> GetByEncodedName(string encodedName)
        {
            return await _dbContext.Cottages
                .Include(c => c.ContactDetails)
                .Include(c => c.Images)
                .FirstOrDefaultAsync(c => c.EncodedName == encodedName);
        }

        public async Task Update(Cottage cottage)
        {
            _dbContext.Cottages.Update(cottage);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Commit()
        {
            await _dbContext.SaveChangesAsync();
        }

        // Implementacja metody dodającej zdjęcie do tabeli CottageImages
        public async Task AddImage(CottageImage image)
        {
            _dbContext.CottageImages.Add(image);
            // Tu nie dajemy SaveChangesAsync, bo Commit() zrobi to na końcu pętli w Handlerze
        }

        public async Task Delete(Cottage cottage)
        {
            _dbContext.Cottages.Remove(cottage);
            await _dbContext.SaveChangesAsync();
        }
    }
}