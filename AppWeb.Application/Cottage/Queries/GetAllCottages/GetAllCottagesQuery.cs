using AppWeb.Application.DataTransferObject;
using MediatR;

namespace AppWeb.Application.Cottage.Queries.GetAllCottages
{
    // To zapytanie nie potrzebuje parametrów, bo chcemy po prostu "wszystko"
    public class GetAllCottagesQuery : IRequest<IEnumerable<CottageDto>>
    {


    }
}