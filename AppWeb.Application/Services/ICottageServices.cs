using AppWeb.Application.DataTransferObject;

namespace AppWeb.Application.Services
{
    public interface ICottageServices
    {
        Task Create(CottageDto cottageDto);
        Task<IEnumerable<CottageDto>> GetAllCottage();
        Task AddImages(string encodedName, List<string> imageUrls);

        // Twoja nazwa metody:
        Task<CottageDto> GetMoreForCottage(string encodedName);
    }
}