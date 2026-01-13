using AppWeb.Application.DataTransferObject;
using AppWeb.Domain.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace AppWeb.Application.Cottage.Queries.GetUserCottages
{
    public class GetUserCottagesQueryHandler : IRequestHandler<GetUserCottagesQuery, IEnumerable<CottageDto>>
    {
        private readonly ICottageRepository _cottageRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetUserCottagesQueryHandler(ICottageRepository cottageRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _cottageRepository = cottageRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IEnumerable<CottageDto>> Handle(GetUserCottagesQuery request, CancellationToken cancellationToken)
        {
            // Pobranie ID zalogowanego użytkownika
            var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Enumerable.Empty<CottageDto>();
            }

            // Pobieramy domki z bazy
            var allCottages = await _cottageRepository.GetAllCottage();

            // Filtrujemy tylko te, które należą do usera
            // Zakładam, że w encji Cottage masz pole CreatedById
            var userCottages = allCottages.Where(c => c.CreatedById == userId);

            return _mapper.Map<IEnumerable<CottageDto>>(userCottages);
        }
    }
}