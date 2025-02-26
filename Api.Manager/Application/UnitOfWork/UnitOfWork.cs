using Api.Manager.Domain.Interfaces;
using Api.Manager.Infraestructure.Dbo;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Api.Manager.Application.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbConnection _connection;
        private readonly IDbTransaction _transaction;
        private bool _disposed;

        private IHotelRepository _hotelRepository;
        private IBookingRepository _bookingRepository;
        private IGuestRepository _guestRepository;
        private IUserRepository _userRepository;
        private IRoomRepository _roomRepository;
        private IRoleRepository _roleRepository;
        private IEmergencyContactRepository _emergencyContactRepository;

        public UnitOfWork(IConfiguration configuration)
        {
            _connection = new SqlConnection(configuration.GetConnectionString("ConnectionBD"));
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }

        public IHotelRepository HotelRepository => _hotelRepository ??= new HotelRepository(_connection, _transaction);
        public IBookingRepository BookingRepository => _bookingRepository ??= new BookingRepository(_connection, _transaction);
        public IGuestRepository GuestRepository => _guestRepository ??= new GuestRepository(_connection, _transaction);
        public IUserRepository UserRepository => _userRepository ??= new UserRepository(_connection, _transaction);
        public IRoomRepository RoomRepository => _roomRepository ??= new RoomRepository(_connection, _transaction);
        public IRoleRepository RoleRepository => _roleRepository ??= new RoleRepository(_connection, _transaction);
        public IEmergencyContactRepository EmergencyContactRepository => _emergencyContactRepository ??= new EmergencyContactRepository(_connection, _transaction);


        public async Task<int> SaveChangesAsync()
        {
            try
            {
                _transaction.Commit();
                return 1;
            }
            catch
            {
                _transaction.Rollback();
                return 0;
            }
            finally
            {
                Dispose();
            }
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                _transaction.Dispose();
                _connection.Dispose();
                _disposed = true;
            }
        }
    }
}
