using AppWeb.Application.DataTransferObject;
using Microsoft.AspNetCore.Http; // tyczy sie uploadu plików (path: imageFiles) na serwer

namespace AppWeb.Application.Services
{
    public interface ICottageServices
    {
        // Dodajemy drugi parametr: ImageFiles
        Task Create(CottageDto cottage, List<IFormFile> ImageFiles);
    }
}