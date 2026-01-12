using MediatR;

namespace AppWeb.Application.Cottage.Commands.AddCottageImages
{
    public class AddCottageImagesCommand : IRequest
    {
        public string EncodedName { get; set; } = default!;
        public List<string> ImageUrls { get; set; } = new();
    }
}