using FluentValidation;


namespace AppWeb.Application.Cottage.Commands.CreateCottage
{
    public class CreateCottageCommandValidator : AbstractValidator<CreateCottageCommand>
    {

        public CreateCottageCommandValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Proszę podać nazwę domku.")
                .Length(2, 30).WithMessage("Nazwa domku musi zawierać od 2 do 30 znaków.");

            // Cena - sprawdzamy czy większa od zera
            RuleFor(c => c.Price)
                .GreaterThan(0).WithMessage("Cena musi być większa niż 0.");

            RuleFor(c => c.Description)
                .MaximumLength(100).WithMessage("Krótki opis może mieć maksymalnie 100 znaków.");

            RuleFor(c => c.About)
                .MaximumLength(1000).WithMessage("Szczegóły obiektu mogą mieć maksymalnie 1000 znaków.");

            RuleFor(c => c.MaxPersons)
                .NotEmpty().WithMessage("Proszę podać maksymalną liczbę osób.")
                .InclusiveBetween(1, 50).WithMessage("Liczba osób musi być między 1 a 50.");

            RuleFor(c => c.Street)
                .NotEmpty().WithMessage("Proszę podać ulicę.");

            RuleFor(c => c.City)
                .NotEmpty().WithMessage("Proszę podać miejscowość.")
                .Length(2, 50).WithMessage("Miejscowość musi mieć od 2 do 50 znaków.");

            RuleFor(c => c.PostalCode)
                .NotEmpty().WithMessage("Proszę podać kod pocztowy.")
                .Matches(@"^\d{2}-\d{3}$").WithMessage("Kod pocztowy musi mieć format XX-XXX.");
        }
    }
}