using Api.Manager.Domain.Entity;
using Api.Manager.Domain.Exceptions;
using Api.Manager.Domain.Interfaces;
using Api.Manager.Domain.Records;
using Dapper;
using Newtonsoft.Json;
using System.Data;

namespace Api.Manager.Infraestructure.Dbo
{
    public class BookingRepository : IBookingRepository
    {
        private readonly IDbConnection _connection;
        private readonly IDbTransaction _transaction;

        private readonly string name_db = "DB_Hotel";

        public BookingRepository(IDbConnection connection, IDbTransaction transaction)
        {
            _connection = connection;
            _transaction = transaction;
        }

        public int CreateBooking(string json)
        {
            var param = new DynamicParameters();
            param.Add(name: "@IN_DATA", json, dbType: DbType.String, size: int.MaxValue, direction: ParameterDirection.Input);
            param.Add(name: "@OUT_ID", dbType: DbType.Int32, size: int.MaxValue, direction: ParameterDirection.Output);
            param.Add(name: "@OUT_ERROR_MESSAGE", dbType: DbType.String, size: int.MaxValue, direction: ParameterDirection.Output);

            _connection.Execute(
                $"{name_db}.[dbo].[BOOKING_INSERT]",
                param,
                commandType: CommandType.StoredProcedure,
                transaction: _transaction);

            if (!string.IsNullOrEmpty(param.Get<string>("OUT_ERROR_MESSAGE")))
                throw new ApiException(param.Get<string>("OUT_ERROR_MESSAGE"));

            return param.Get<int>("@OUT_ID");
        }

        public void CreateBookingRooms(int room, int booking)
        {
            var param = new DynamicParameters();
            param.Add(name: "@ROOM_ID", room, dbType: DbType.Int32, size: int.MaxValue, direction: ParameterDirection.Input);
            param.Add(name: "@BOOKING_ID", booking, dbType: DbType.Int32, size: int.MaxValue, direction: ParameterDirection.Input);
            param.Add(name: "@OUT_ERROR_MESSAGE", dbType: DbType.String, size: 4000, direction: ParameterDirection.Output);

            _connection.Execute(
                $"{name_db}.[dbo].[BOOKINGROOMS_INSERT]",
                param,
                commandType: CommandType.StoredProcedure,
                transaction: _transaction);

            if (!string.IsNullOrEmpty(param.Get<string>("OUT_ERROR_MESSAGE")))
                throw new ApiException(param.Get<string>("OUT_ERROR_MESSAGE"));
        }

        public List<BookingEntity> GetAll()
        {
            var param = new DynamicParameters();
            param.Add(name: "@OUT_DATA", dbType: DbType.String, size: int.MaxValue, direction: ParameterDirection.Output);

            _connection.Execute(
                $"{name_db}.[dbo].[BOOKING_GET_ALL]",
                param,
                commandType: CommandType.StoredProcedure,
                transaction: _transaction);

            var result = JsonConvert.DeserializeObject<List<BookingRecord>>(param.Get<string>("@OUT_DATA")).DefaultIfEmpty();

            return result.Select(BookingEntity.CreateFromRecord).ToList();
        }

        public BookingEntity GetById(int id)
        {
            var param = new DynamicParameters();
            param.Add(name: "@RESERVATION_ID", id, dbType: DbType.Int32, size: int.MaxValue, direction: ParameterDirection.Input);
            param.Add(name: "@OUT_DATA", dbType: DbType.String, size: int.MaxValue, direction: ParameterDirection.Output);

            _connection.Execute(
                $"{name_db}.[dbo].[BOOKING_GETBY_ID]",
                param,
                commandType: CommandType.StoredProcedure,
                transaction: _transaction);

            BookingRecord result = new BookingRecord();
            if (!string.IsNullOrEmpty(param.Get<string>("@OUT_DATA")))
            {
                result = JsonConvert.DeserializeObject<BookingRecord>(param.Get<string>("@OUT_DATA"));
                return BookingEntity.CreateFromRecord(result);
            }

            return BookingEntity.NoExistsRecord(0);
        }
    }
}
