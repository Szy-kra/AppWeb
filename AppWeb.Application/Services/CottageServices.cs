using AppWeb.Application.DataTransferObject;
using AppWeb.Domain.Entities;
using AppWeb.Domain.Interfaces;
using AutoMapper;

namespace AppWeb.Application.Services
{
    public class CottageServices : ICottageServices
    {
        private readonly ICottageRepository _cottageRepository;
        private readonly IMapper _mapper;

        public CottageServices(ICottageRepository cottageRepository, IMapper mapper)
        {
            _cottageRepository = cottageRepository;
            _mapper = mapper;
        }

        public async Task Create(CottageDto cottageDto)
        {
            var cottage = _mapper.Map<Cottage>(cottageDto);
            cottage.EncodeName();
            await _cottageRepository.Create(cottage);
        }

        public async Task<IEnumerable<CottageDto>> GetAllCottage()
        {
            var cottages = await _cottageRepository.GetAllCottage();
            var cottageDtos = _mapper.Map<IEnumerable<CottageDto>>(cottages);
            return cottageDtos;
        }

        public async Task AddImages(string encodedName, List<string> imageUrls)
        {
            var allCottages = await _cottageRepository.GetAllCottage();
            var cottage = allCottages.FirstOrDefault(c => c.EncodedName == encodedName);

            if (cottage != null && imageUrls != null && imageUrls.Any())
            {
                foreach (var url in imageUrls)
                {
                    cottage.Images.Add(new CottageImage
                    {
                        Url = url,
                        CottageId = cottage.Id
                    });
                }
                await _cottageRepository.Update(cottage);
            }
        }

        // TA METODA NAPRAWIA BŁĄD KOMPILACJI
        public async Task<CottageDto> GetMoreForCottage(string encodedName)
        {
            // Pobieramy wszystkie, bo GetAllCottage w repozytorium ma już Include(Images) i Include(Details)
            var allCottages = await _cottageRepository.GetAllCottage();
            var cottage = allCottages.FirstOrDefault(c => c.EncodedName == encodedName);

            if (cottage == null) return null;

            return _mapper.Map<CottageDto>(cottage);
        }
    }
}