using AppWeb.Application.DataTransferObject;
using MediatR;

namespace AppWeb.Application.Cottage.Commands.CreateCottage
{
    // Musisz dodać dziedziczenie po CottageDto (aby mieć pola Name, Price itd.)
    // ORAZ implementację IRequest<Unit> (aby MediatR wiedział, że to komenda)
    public class CreateCottageCommand : CottageDto, IRequest<Unit>
    {
        // Klasa może zostać pusta, bo dziedziczy pola z CottageDto
    }
}