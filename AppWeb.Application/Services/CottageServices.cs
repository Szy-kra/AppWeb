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

        public async Task<IEnumerable<CottageDto>> GetAllCottage()
        {
            var allCottagesFromDb = await _cottageRepository.GetAllCottage();
            return _mapper.Map<IEnumerable<CottageDto>>(allCottagesFromDb);
        }

        public async Task Create(CottageDto cottageDto, List<IFormFile>? ImageFiles)
        {
            // Inicjalizacja listy, jeśli jest nullem
            cottageDto.ImageUrls ??= new List<string>();

            if (ImageFiles != null && ImageFiles.Count > 0)
            {
                // Ścieżka do folderu wwwroot/DataImage
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "DataImage");

                // Automatyczne tworzenie folderu, jeśli go nie ma
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                foreach (var file in ImageFiles)
                {
                    if (file.Length > 0)
                    {
                        // Tworzymy unikalną nazwę pliku
                        string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                        string filePath = Path.Combine(uploadsFolder, fileName);

                        // Zapis fizyczny na dysku
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                        }

                        // WAŻNE: Zapisujemy do bazy TYLKO nazwę pliku
                        cottageDto.ImageUrls.Add(fileName);
                    }
                }
            }

            // Mapowanie DTO -> Encja i zapis w bazie
            var cottage = _mapper.Map<AppWeb.Domain.Entities.Cottage>(cottageDto);
            await _cottageRepository.Create(cottage);
        }
    }
}