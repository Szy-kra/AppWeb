using AppWeb.Application.DataTransferObject;
using AppWeb.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AppWeb.Application.Cottage.Queries.GetForOneCottage
{
    public class GetForOneCottageQueryHandler : IRequestHandler<GetForOneCottageQuery, CottageDto>
    {
        private readonly ICottageRepository _cottageRepository;
        private readonly IMapper _mapper;

        public GetForOneCottageQueryHandler(ICottageRepository cottageRepository, IMapper mapper)
        {
            _cottageRepository = cottageRepository;
            _mapper = mapper;
        }

        public async Task<CottageDto> Handle(GetForOneCottageQuery request, CancellationToken cancellationToken)
        {
            // Pobieramy dane z bazy
            var allCottages = await _cottageRepository.GetAllCottage();

            // Szukamy tego jednego konkretnego po nazwie (EncodedName) przekazanej w zapytaniu (request)
            var cottage = allCottages.FirstOrDefault(c => c.EncodedName == request.EncodedName);

            if (cottage == null)
            {
                return null;
            }

            // Mapujemy ten jeden obiekt na DTO
            var cottageDto = _mapper.Map<CottageDto>(cottage);

            return cottageDto;
        }
    }
}