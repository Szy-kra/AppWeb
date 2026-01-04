namespace AppWeb.Domain.Interfaces
{

    public interface ICottageRepository
    {
        Task Create(Domain.Entities.Cottage cottage);
    }

}