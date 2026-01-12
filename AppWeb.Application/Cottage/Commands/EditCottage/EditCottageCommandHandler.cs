using AppWeb.Domain.Interfaces;
using MediatR;

namespace AppWeb.Application.Cottage.Commands.EditCottage
{
    // Upewnij się, że klasa jest wewnątrz namespace i nie ma zbędnych znaków przed 'public'
    public class EditCottageCommandHandler : IRequestHandler<EditCottageCommand, Unit>
    {
        private readonly ICottageRepository _repository;

        public EditCottageCommandHandler(ICottageRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(EditCottageCommand request, CancellationToken cancellationToken)
        {
            var cottage = await _repository.GetByEncodedName(request.EncodedName);
            if (cottage == null) return Unit.Value;

            cottage.Name = request.Name;
            cottage.About = request.About;
            cottage.ContactDetails.Description = request.Description;
            cottage.ContactDetails.Price = request.Price;
            cottage.ContactDetails.City = request.City;
            cottage.ContactDetails.Street = request.Street;
            cottage.ContactDetails.PostalCode = request.PostalCode;
            cottage.ContactDetails.MaxPersons = request.MaxPersons;

            cottage.EncodeName();
            await _repository.Commit();

            return Unit.Value;
        }
    }
}