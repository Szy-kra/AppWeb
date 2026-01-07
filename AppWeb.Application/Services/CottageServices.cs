using AppWeb.Application.DataTransferObject;
using AppWeb.Domain.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Hosting; // Potrzebne do IWebHostEnvironment
using Microsoft.AspNetCore.Http;    // Potrzebne do IFormFile

namespace AppWeb.Application.Services
{

    public class CottageServices : ICottageServices
    {
        private readonly ICottageRepository _cottageRepository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment; // Pole do obsługi ścieżek plików

        public CottageServices(ICottageRepository cottageRepository, IMapper mapper, IWebHostEnvironment webHostEnvironment)
        {
            _cottageRepository = cottageRepository;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment; // Wstrzyknięcie środowiska
        }


        // Metoda, która zapisuje fizyczne pliki z pamięci RAM na dysk serwera, 
        // a ich ścieżki (tekst) utrwala w bazie danych.
        public async Task Create(CottageDto cottageDto, List<IFormFile> ImageFiles)
        {
            // 1. Logika zapisu plików na dysk
            if (ImageFiles != null && ImageFiles.Count > 0)
            {
                foreach (var file in ImageFiles)
                {
                    if (file.Length > 0)
                    {
                        // Generowanie unikalnej nazwy, żeby zdjęcia się nie nadpisały
                        string fileName = Guid.NewGuid().ToString() + "_" + file.FileName;

                        // Ścieżka do Twojego folderu w wwwroot
                        string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploadsImg");
                        string filePath = Path.Combine(uploadsFolder, fileName);

                        // Fizyczny zapis pliku na serwerze
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                        }

                        // Dodanie ścieżki/nazwy do DTO, aby Mapper wiedział co zapisać w bazie
                        cottageDto.ImageUrls.Add("/uploadsImg/" + fileName);
                    }
                }
            }

            // 2. Mapowanie i zapis do bazy danych
            var cottage = _mapper.Map<Domain.Entities.Cottage>(cottageDto);
            cottage.EncodeName();

            await _cottageRepository.Create(cottage);
        }
    }
}