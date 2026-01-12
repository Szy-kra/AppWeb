using AppWeb.Domain.Entities;
using AppWeb.Domain.Interfaces;
using MediatR;

namespace AppWeb.Application.Cottage.Commands.AddCottageImages
{
    public class AddCottageImagesCommandHandler : IRequestHandler<AddCottageImagesCommand>
    {
        private readonly ICottageRepository _cottageRepository;

        public AddCottageImagesCommandHandler(ICottageRepository cottageRepository)
        {
            _cottageRepository = cottageRepository;
        }

        public async Task Handle(AddCottageImagesCommand request, CancellationToken cancellationToken)
        {
            var allCottages = await _cottageRepository.GetAllCottage();
            var cottage = allCottages.FirstOrDefault(c => c.EncodedName == request.EncodedName);

            if (cottage != null && request.ImageUrls.Any())
            {
                foreach (var url in request.ImageUrls)
                {
                    cottage.Images.Add(new CottageImage
                    {
                        Url = url,
                        CottageId = cottage.Id
                    });
                }
                await _cottageRepository.Update(cottage);
            }
        }
    }
}