using AppWeb.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AppWeb.Application.Cottage.Commands.CreateCottage
{
    public class CreateCottageCommandHandler : IRequestHandler<CreateCottageCommand, string>
    {
        private readonly ICottageRepository _cottageRepository;
        private readonly IMapper _mapper;
        private readonly IUserContext _userContext;

        public CreateCottageCommandHandler(ICottageRepository cottageRepository, IMapper mapper, IUserContext userContext)
        {
            _cottageRepository = cottageRepository;
            _mapper = mapper;
            _userContext = userContext;
        }

        public async Task<string> Handle(CreateCottageCommand request, CancellationToken cancellationToken)
        {
            // 1. Mapujemy dane z komendy na encję domku
            var cottage = _mapper.Map<AppWeb.Domain.Entities.Cottage>(request);

            // 2. Generujemy przyjazny URL (EncodedName) na podstawie nazwy
            cottage.EncodeName();

            // 3. Pobieramy zalogowanego użytkownika (Twoja metoda: GerCurrentUser)
            var currentUser = _userContext.GerCurrentUser();

            // 4. Przypisujemy ID twórcy (Twoje pole: UseId)
            cottage.CreatedById = currentUser.UseId;

            // 5. Dodajemy domek do repozytorium
            await _cottageRepository.Create(cottage);

            // 6. Zapisujemy zmiany w bazie (Twoja metoda: Commit)
            await _cottageRepository.Commit();

            // 7. Zwracamy nową nazwę, aby kontroler mógł otworzyć stronę ze zdjęciami
            return cottage.EncodedName!;
        }
    }
}