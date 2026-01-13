using AppWeb.Application.DataTransferObject;
using MediatR;

namespace AppWeb.Application.Cottage.Queries.GetUserCottages
{
    public class GetUserCottagesQuery : IRequest<IEnumerable<CottageDto>>
    {
    }
}