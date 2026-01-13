using MediatR;

namespace AppWeb.Application.Cottage.Queries.GetCottageImages
{
    public class GetCottageImagesQuery : IRequest<IEnumerable<string>>
    {
        public string EncodedName { get; set; }

        public GetCottageImagesQuery(string encodedName)
        {
            EncodedName = encodedName;
        }
    }
}