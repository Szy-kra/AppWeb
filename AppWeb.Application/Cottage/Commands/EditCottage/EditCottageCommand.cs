using AppWeb.Application.DataTransferObject;
using MediatR;

namespace AppWeb.Application.Cottage.Commands.EditCottage
{
    public class EditCottageCommand : CottageDto, IRequest<string>
    {
        // Słowo 'new' rozwiązuje konflikt z polem w CottageDto
        public new string EncodedName { get; set; } = default!;
    }
}