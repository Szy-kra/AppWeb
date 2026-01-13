using AppWeb.Domain.Entities;
using AppWeb.Domain.Interfaces;
using MediatR;

namespace AppWeb.Application.Cottage.Commands.AddCottageImages
{
    public class AddCottageImagesCommandHandler : IRequestHandler<AddCottageImagesCommand, Unit>
    {
        private readonly ICottageRepository _cottageRepository;

        public AddCottageImagesCommandHandler(ICottageRepository cottageRepository)
        {
            _cottageRepository = cottageRepository;
        }

        public async Task<Unit> Handle(AddCottageImagesCommand request, CancellationToken cancellationToken)
        {
            var cottage = await _cottageRepository.GetByEncodedName(request.EncodedName);

            if (cottage == null) return Unit.Value;

            foreach (var path in request.ImagePaths)
            {
                var image = new CottageImage
                {
                    Url = path,
                    CottageId = cottage.Id
                };

                await _cottageRepository.AddImage(image);
            }

            await _cottageRepository.Commit();

            return Unit.Value;
        }
    }
}