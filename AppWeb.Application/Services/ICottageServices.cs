using AppWeb.Application.DataTransferObject;
using Microsoft.AspNetCore.Http;

namespace AppWeb.Application.Services
{
    public interface ICottageServices
    {
        // Tworzenie domku z obsługą plików - dodano '?' przy liście, bo zdjęcia są opcjonalne
        Task Create(CottageDto cottage, List<IFormFile>? ImageFiles);

        // Pobieranie listy wszystkich domków do wyświetlenia na Indexie
        Task<IEnumerable<CottageDto>> GetCottageList();
    }
}