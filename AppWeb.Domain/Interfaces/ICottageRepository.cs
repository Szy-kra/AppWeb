using AppWeb.Domain.Entities;

namespace AppWeb.Domain.Interfaces
{
    public interface ICottageRepository
    {
        // Kontrakt na tworzenie domku
        Task Create(Cottage cottage);

        // Kontrakt na pobieranie wszystkich domków
        Task<IEnumerable<Cottage>> GetAllCottage();

        // POPRAWIONE: Dodano kontrakt na aktualizację (niezbędne do zapisu zdjęć)
        Task Update(Cottage cottage);
    }
}