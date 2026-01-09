using AppWeb.Application.DataTransferObject;
using Microsoft.AspNetCore.Http;

namespace AppWeb.Application.Services
{
    public interface ICottageServices
    {
        // Tworzenie domku z obsługą plików
        Task Create(CottageDto cottage, List<IFormFile> ImageFiles);

        // Pobieranie listy wszystkich domków
        Task<IEnumerable<CottageDto>> GetCottageList();
    }
}