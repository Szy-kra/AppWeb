using AppWeb.Domain.Interfaces;
using MediatR;

namespace AppWeb.Application.Cottage.Queries.GetCottageImages
{
    public class GetCottageImagesQueryHandler : IRequestHandler<GetCottageImagesQuery, IEnumerable<string>>
    {
        private readonly ICottageRepository _cottageRepository;

        public GetCottageImagesQueryHandler(ICottageRepository cottageRepository)
        {
            _cottageRepository = cottageRepository;
        }

        public async Task<IEnumerable<string>> Handle(GetCottageImagesQuery request, CancellationToken cancellationToken)
        {
            var cottage = await _cottageRepository.GetByEncodedName(request.EncodedName);

            if (cottage == null || cottage.Images == null)
            {
                return Enumerable.Empty<string>();
            }

            // Wyciągamy same ścieżki URL z encji zdjęć
            return cottage.Images.Select(i => i.Url).ToList();
        }
    }
}