using AppWeb.Domain.Entities;

namespace AppWeb.Domain.Interfaces
{
    public interface ICottageRepository
    {
        // Tworzenie domku
        Task Create(Cottage cottage);

        // Pobieranie wszystkich domków
        Task<IEnumerable<Cottage>> GetAllCottage();

        // Pobieranie jednego konkretnego domku po unikalnej nazwie
        Task<Cottage?> GetByEncodedName(string encodedName);

        // Kontrakt na aktualizację
        Task Update(Cottage cottage);

        // Zapisywanie zmian w bazie (Zatwierdzenie transakcji)
        Task Commit();

        // --- BRAKUJĄCA METODA DO ZDJĘĆ ---
        Task AddImage(CottageImage image);

        Task Delete(Cottage cottage); //metoda usuwania reokordu domku
    }
}