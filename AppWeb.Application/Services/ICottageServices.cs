using AppWeb.Application.DataTransferObject;
using Microsoft.AspNetCore.Http;

namespace AppWeb.Application.Services
{
    public interface ICottageServices
    {
        Task Create(CottageDto cottageDto, List<IFormFile>? ImageFiles);
        Task<IEnumerable<CottageDto>> GetAllCottage();
    }
}