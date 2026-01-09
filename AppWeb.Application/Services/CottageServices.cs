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
            var cottages = await _cottageRepository.GetAll();
            return _mapper.Map<IEnumerable<CottageDto>>(cottages);
        }

        public async Task Create(CottageDto cottageDto, List<IFormFile>? ImageFiles)
        {
            // 1. Zabezpieczenie listy URLi (inicjalizacja, jeśli jest nullem)
            cottageDto.ImageUrls ??= new List<string>();

            // 2. Logika zapisu plików na dysk (wykona się tylko, jeśli zdjęcia zostały przesłane)
            if (ImageFiles != null && ImageFiles.Any())
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "DataImage");

                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                foreach (var file in ImageFiles)
                {
                    if (file.Length > 0)
                    {
                        string fileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                        string filePath = Path.Combine(uploadsFolder, fileName);

                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                        }

                        // Dodajemy ścieżkę do DTO, którą Mapper później zamieni na rekord w bazie
                        cottageDto.ImageUrls.Add("/DataImage/" + fileName);
                    }
                }
            }

            // 3. Mapowanie DTO na Encję Domain
            // AutoMapper w profilu (CottageMappingProfile) zajmie się resztą: 
            // - przekopiuje dane adresowe do ContactDetails
            // - stworzy obiekty CottageImage z listy ImageUrls
            // - wygeneruje EncodedName (Slug)
            var cottage = _mapper.Map<AppWeb.Domain.Entities.Cottage>(cottageDto);

            // 4. ZAPIS DO BAZY przez Repozytorium
            await _cottageRepository.Create(cottage);
        }
    }
}