using AppWeb.Application.DataTransferObject;
using AppWeb.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AppWeb.Application.Cottage.Queries.GetForOneToEdit
{
    public class GetForOneToEditQueryHandler : IRequestHandler<GetForOneToEditQuery, CottageDto>
    {
        private readonly ICottageRepository _repository;
        private readonly IMapper _mapper;

        public GetForOneToEditQueryHandler(ICottageRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CottageDto> Handle(GetForOneToEditQuery request, CancellationToken cancellationToken)
        {
            // Pobieramy z bazy (już masz metodę GetByEncodedName w repozytorium)
            var cottage = await _repository.GetByEncodedName(request.EncodedName);

            // Mapujemy na DTO
            return _mapper.Map<CottageDto>(cottage);
        }
    }
}