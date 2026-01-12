using AppWeb.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AppWeb.Application.Cottage.Commands.CreateCottage
{
    public class CreateCottageCommandHandler : IRequestHandler<CreateCottageCommand, Unit>
    {
        private readonly ICottageRepository _cottageRepository;
        private readonly IMapper _mapper;

        public CreateCottageCommandHandler(ICottageRepository cottageRepository, IMapper mapper)
        {
            _cottageRepository = cottageRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(CreateCottageCommand request, CancellationToken cancellationToken)
        {
            // 1. Mapujemy komendę na encję bazodanową
            var cottage = _mapper.Map<AppWeb.Domain.Entities.Cottage>(request);

            // 2. Tworzymy przyjazny adres URL (EncodedName)
            cottage.EncodeName();

            // 3. ZAPISUJEMY ASYNCHRONICZNIE (To usuwa warning CS1998)
            await _cottageRepository.Create(cottage);

            // 4. Zwracamy informację o zakończeniu zadania
            return Unit.Value;
        }
    }
}