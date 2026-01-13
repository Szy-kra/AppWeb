using AppWeb.Domain.Interfaces; // upewnij się, że masz tu dostęp do repozytorium
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace AppWeb.Application.Cottage.Queries.GetContact
{
    public class GetContactQueryHandler : IRequestHandler<GetContactQuery, CurrentUser>
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ICottageRepository _repository;

        public GetContactQueryHandler(UserManager<IdentityUser> userManager, ICottageRepository repository)
        {
            _userManager = userManager;
            _repository = repository;
        }

        public async Task<CurrentUser> Handle(GetContactQuery request, CancellationToken cancellationToken)
        {
            // 1. Szukamy domku w bazie danych po encodedName
            var cottage = await _repository.GetByEncodedName(request.EncodedName);

            if (cottage == null || string.IsNullOrEmpty(cottage.CreatedById))
            {
                return new CurrentUser("0", "Nieznany", "Brak danych", "Brak danych");
            }

            // 2. Szukamy właściciela tego domku w bazie Identity
            var owner = await _userManager.FindByIdAsync(cottage.CreatedById);

            if (owner == null)
            {
                return new CurrentUser("0", "Nieznany", "Brak danych", "Brak danych");
            }

            // 3. Zwracamy dane WŁAŚCICIELA (w tym telefon z bazy)
            return new CurrentUser(
                owner.Id,
                owner.UserName ?? "Brak",
                owner.Email ?? "Brak",
                owner.PhoneNumber ?? "Nie podano"
            );
        }
    }
}