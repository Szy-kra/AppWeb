using AppWeb.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace AppWeb.Application.Cottage.Commands.DeleteCottage
{
    // UPEWNIJ SIĘ, ŻE SĄ TU DWA PARAMETRY: <DeleteCottageCommand, Unit>
    public class DeleteCottageCommandHandler : IRequestHandler<DeleteCottageCommand, Unit>
    {
        private readonly ICottageRepository _cottageRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DeleteCottageCommandHandler(ICottageRepository cottageRepository, IHttpContextAccessor httpContextAccessor)
        {
            _cottageRepository = cottageRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Unit> Handle(DeleteCottageCommand request, CancellationToken cancellationToken)
        {
            var cottage = await _cottageRepository.GetByEncodedName(request.EncodedName);
            var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Zabezpieczenie: tylko właściciel może usunąć swój domek
            if (cottage != null && cottage.CreatedById == userId)
            {
                await _cottageRepository.Delete(cottage);
            }

            // To musi zwracać Unit.Value
            return Unit.Value;
        }
    }
}