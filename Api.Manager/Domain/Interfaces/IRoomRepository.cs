using Api.Manager.Domain.Entity;

namespace Api.Manager.Domain.Interfaces
{
    public interface IRoomRepository
    {
        List<RoomEntity> GetAll();
        List<TypeRoomEntity> GetAllTypeRooms();
        RoomEntity GetById(int idRoom, int idHotel);
        int Create(string json);
        bool UpdateRoom(string json);
        void UpdateStateRoom(int id, bool isActive);
        List<RoomFilterEntity> FindByFilters(string city, DateTime init, DateTime end);
    }
}
