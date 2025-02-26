using Api.Manager.Domain.Entity;

namespace Api.Manager.Domain.Interfaces
{
    public interface IGuestRepository
    {
        void CreateGuest(string json, int booking);
        List<GuestEntity> GetAll();
    }
}
