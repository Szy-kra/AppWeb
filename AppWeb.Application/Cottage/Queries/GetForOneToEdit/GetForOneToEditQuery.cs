using AppWeb.Application.DataTransferObject;
using MediatR;

namespace AppWeb.Application.Cottage.Queries.GetForOneToEdit
{
    public class GetForOneToEditQuery : IRequest<CottageDto>
    {
        public string EncodedName { get; }

        public GetForOneToEditQuery(string encodedName)
        {
            EncodedName = encodedName;
        }
    }
}