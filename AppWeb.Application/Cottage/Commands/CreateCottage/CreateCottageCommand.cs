using AppWeb.Application.DataTransferObject;
using MediatR;

namespace AppWeb.Application.Cottage.Commands.CreateCottage
{
    public class CreateCottageCommand : CottageDto, IRequest<string>
    {
        // Klasa dziedziczy pola z CottageDto. 
        // Zwraca string (EncodedName) po utworzeniu.
    }
}