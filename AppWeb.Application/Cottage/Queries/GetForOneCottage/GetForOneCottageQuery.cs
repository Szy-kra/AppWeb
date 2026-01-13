using AppWeb.Application.DataTransferObject;
using MediatR;

namespace AppWeb.Application.Cottage.Queries.GetForOneCottage
{
    public class GetForOneCottageQuery : IRequest<CottageDto>
    {
        public string EncodedName { get; set; }

        public GetForOneCottageQuery(string encodedName)
        {
            EncodedName = encodedName;
        }
    }
}