using AppWeb.Application.DataTransferObject;
using AppWeb.Domain.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace AppWeb.Application.Services
{
    public class CottageServices : ICottageServices
    {
        private readonly ICottageRepository _cottageRepository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CottageServices(ICottageRepository cottageRepository, IMapper mapper, IWebHostEnvironment webHostEnvironment)
        {
            _cottageRepository = cottageRepository;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IEnumerable<CottageDto>> GetCottageList()
        {
            // Pobranie encji z bazy (Domain)
            var cottages = await _cottageRepository.GetAll();

            // Mapowanie na DTO (Application)
            var dtos = _mapper.Map<IEnumerable<CottageDto>>(cottages);

            return dtos;
        }

        public async Task Create(CottageDto cottageDto, List<IFormFile> ImageFiles)
        {
            // 1. Logika zapisu plików na dysk
            if (ImageFiles != null && ImageFiles.Count > 0)
            {
                foreach (var file in ImageFiles)
                {
                    if (file.Length > 0)
                    {
                        string fileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                        string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploadsImg");

                        // Upewnij się, że folder istnieje
                        if (!Directory.Exists(uploadsFolder)) Directory.CreateDirectory(uploadsFolder);

                        string filePath = Path.Combine(uploadsFolder, fileName);

                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                        }

                        cottageDto.ImageUrls.Add("/uploadsImg/" + fileName);
                    }
                }
            }

            // 2. Mapowanie i zapis do bazy danych
            var cottage = _mapper.Map<AppWeb.Domain.Entities.Cottage>(cottageDto);

            // Metoda tworząca przyjazny URL (jeśli masz ją w encji)
            cottage.EncodeName();

            await _cottageRepository.Create(cottage);
        }
    }
}