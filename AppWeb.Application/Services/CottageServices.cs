

using AppWeb.Domain.Interfaces;

namespace AppWeb.Application.Services
{
    public class CottageServices : ICottageServices
    {
        //przekazanie referencji do repozytorium

        private readonly ICottageRepository _cottageRepository;

        public CottageServices(ICottageRepository cottageRepository)
        {
            _cottageRepository = cottageRepository;
        }



        public async Task Create(Domain.Entities.Cottage cottage)
        {
            cottage.EncodeName();

            await _cottageRepository.Create(cottage);
        }





    }

}