using MediatR;

namespace AppWeb.Application.Cottage.Commands.AddCottageImages
{
    public class AddCottageImagesCommand : IRequest<Unit>
    {
        public string EncodedName { get; set; } = default!;
        public List<string> ImagePaths { get; set; } = new();
    }
}