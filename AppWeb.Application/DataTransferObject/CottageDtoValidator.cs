using AppWeb.Application.DataTransferObject;
using FluentValidation;

namespace AppWeb.Application.Validators
{
    // Klasa musi dziedziczyć po AbstractValidator, żeby system ją widział
    public class CottageDtoValidator : AbstractValidator<CottageDto>
    {
        public CottageDtoValidator()
        {
            // Nazwa
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Proszę podać nazwę domku.")
                .Length(2, 30).WithMessage("Nazwa domku musi zawierać od 2 do 30 znaków.");

            // Cena
            RuleFor(c => c.Price)
                .NotEmpty().WithMessage("Proszę podać cenę za dobę.")
                .GreaterThan(0).WithMessage("Cena musi być większa niż 0.");

            // Maksymalna liczba osób
            RuleFor(c => c.MaxPersons)
                .NotEmpty().WithMessage("Proszę podać maksymalną liczbę osób.")
                .InclusiveBetween(1, 50).WithMessage("Liczba osób musi być między 1 a 50.");

            // Ulica
            RuleFor(c => c.Street)
                .NotEmpty().WithMessage("Proszę podać ulicę, przy której znajduje się domek.");

            // Miejscowość
            RuleFor(c => c.City)
                .NotEmpty().WithMessage("Proszę podać miejscowość.")
                .Length(2, 50).WithMessage("Nazwa miejscowości musi zawierać od 2 do 50 znaków.");

            // Kod pocztowy
            RuleFor(c => c.PostalCode)
                .NotEmpty().WithMessage("Proszę podać kod pocztowy.")
                .Matches(@"^\d{2}-\d{3}$").WithMessage("Kod pocztowy musi mieć format XX-XXX.");
        }
    }
}