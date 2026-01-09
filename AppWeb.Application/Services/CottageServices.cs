using AppWeb.Application.DataTransferObject;
using AppWeb.Domain.Entities;
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
            // 1. Mapowanie danych podstawowych
            var cottage = _mapper.Map<Cottage>(cottageDto);

            // Inicjalizacja listy zdjęć w encji
            cottage.Images = new List<CottageImage>();

            // 2. Obsługa plików fizycznych
            if (ImageFiles != null && ImageFiles.Count > 0)
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
                        string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                        string filePath = Path.Combine(uploadsFolder, fileName);

                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                        }

                        // Zapisujemy URL w formacie zgodnym z Twoją bazą danych
                        cottage.Images.Add(new CottageImage
                        {
                            Url = "/DataImage/" + fileName
                        });
                    }
                }
            }

            // 3. Generowanie EncodedName (SEO Slug)
            cottage.EncodeName();

            // 4. Zapis całości przez repozytorium
            await _cottageRepository.Create(cottage);
        }
    }
}