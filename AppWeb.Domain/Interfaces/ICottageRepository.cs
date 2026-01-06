namespace AppWeb.Domain.Interfaces
{

    public interface ICottageRepository
    {
        Task Create(Domain.Entities.Cottage cottage);
        // DODAJEMY TO:
        Task<IEnumerable<Domain.Entities.Cottage>> GetAll();
    }


}