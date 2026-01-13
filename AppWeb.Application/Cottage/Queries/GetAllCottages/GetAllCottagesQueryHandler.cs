using AppWeb.Application.DataTransferObject;
using AppWeb.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AppWeb.Application.Cottage.Queries.GetAllCottages
{
    public class GetAllCottagesQueryHandler : IRequestHandler<GetAllCottagesQuery, IEnumerable<CottageDto>>
    {
        private readonly ICottageRepository _cottageRepository;
        private readonly IMapper _mapper;

        public GetAllCottagesQueryHandler(ICottageRepository cottageRepository, IMapper mapper)
        {
            _cottageRepository = cottageRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CottageDto>> Handle(GetAllCottagesQuery request, CancellationToken cancellationToken)
        {
            // Pobiera wszystkie encje (pamiętaj o .Include(c => c.Images) w repozytorium!)
            var cottages = await _cottageRepository.GetAllCottage();

            // Mapuje całą listę na DTO (w tym zdjęcia, jeśli Mapper jest ustawiony)
            var cottageDtos = _mapper.Map<IEnumerable<CottageDto>>(cottages);

            return cottageDtos;
        }
    }
}