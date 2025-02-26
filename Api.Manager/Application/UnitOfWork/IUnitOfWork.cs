using Api.Manager.Domain.Interfaces;

namespace Api.Manager.Application.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> SaveChangesAsync();
        IHotelRepository HotelRepository { get; }
        IBookingRepository BookingRepository { get; }
        IGuestRepository GuestRepository { get; }
        IUserRepository UserRepository { get; }
        IRoomRepository RoomRepository { get; }
        IRoleRepository RoleRepository { get; }
        IEmergencyContactRepository EmergencyContactRepository { get; }
    }
}
