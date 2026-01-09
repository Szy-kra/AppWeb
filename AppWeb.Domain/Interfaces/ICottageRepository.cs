using AppWeb.Domain.Entities;

namespace AppWeb.Domain.Interfaces
{
    public interface ICottageRepository
    {
        // Kontrakt na tworzenie domku
        Task Create(Cottage cottage);

        // Kontrakt na pobieranie wszystkich domków (nasza nowość!)
        Task<IEnumerable<Cottage>> GetAllCottage();
    }
}