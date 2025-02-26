using Api.Manager.Domain.Entity;

namespace Api.Manager.Domain.Interfaces
{
    public interface IHotelRepository
    {
        List<HotelEntity> GetAll();
        HotelEntity GetById(int id);
        int CreateHotel(string json);
        bool UpdateHotel(string json);
        void UpdateStateHotel(int id, bool isActive);
    }
}
