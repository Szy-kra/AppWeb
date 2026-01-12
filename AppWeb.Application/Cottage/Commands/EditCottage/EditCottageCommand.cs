using AppWeb.Application.DataTransferObject;
using MediatR;

namespace AppWeb.Application.Cottage.Commands.EditCottage
{
    public class EditCottageCommand : CottageDto, IRequest<Unit>
    {
        // To jest klucz, który pozwala nam znaleźć domek w bazie danych
        public string EncodedName { get; set; } = default!;
    }
}