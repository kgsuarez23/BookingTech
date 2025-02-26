using Api.Manager.Domain.Entity;

namespace Api.Manager.Domain.Interfaces
{
    public interface IBookingRepository
    {
        int CreateBooking(string json);
        void CreateBookingRooms(int room, int booking);
        List<BookingEntity> GetAll();
        BookingEntity GetById(int id);
    }
}
