namespace Api.Manager.Domain.Interfaces
{
    public interface IEmergencyContactRepository
    {
        void Create(string json, int idBooking);
    }
}
