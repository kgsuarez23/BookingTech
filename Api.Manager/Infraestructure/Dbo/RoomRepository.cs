using Api.Manager.Domain.Entity;
using Api.Manager.Domain.Exceptions;
using Api.Manager.Domain.Interfaces;
using Api.Manager.Domain.Records;
using Dapper;
using Newtonsoft.Json;
using System.Data;

namespace Api.Manager.Infraestructure.Dbo
{
    public class RoomRepository : IRoomRepository
    {
        private readonly IDbConnection _connection;
        private readonly IDbTransaction _transaction;

        private readonly string name_db = "DB_Hotel";

        public RoomRepository(IDbConnection connection, IDbTransaction transaction)
        {
            _connection = connection;
            _transaction = transaction;
        }

        public List<RoomEntity> GetAll()
        {
            var param = new DynamicParameters();
            param.Add(name: "@OUT_DATA", dbType: DbType.String, size: int.MaxValue, direction: ParameterDirection.Output);

            _connection.Execute(
                $"{name_db}.[dbo].[ROOM_GET_ALL]",
                param,
                commandType: CommandType.StoredProcedure,
                transaction: _transaction);

            var result = JsonConvert.DeserializeObject<List<RoomRecord>>(param.Get<string>("@OUT_DATA")).DefaultIfEmpty();

            return result.Select(RoomEntity.CreateFromRecord).ToList();
        }

        public List<TypeRoomEntity> GetAllTypeRooms()
        {
            var records = _connection.Query<TypeRoomRecord>(
                $"{name_db}.[dbo].[ROOM_GET_ALL_TYPES]",
                commandType: CommandType.StoredProcedure,
                transaction: _transaction);

            return records.Select(TypeRoomEntity.CreateFromRecord).ToList();
        }

        public RoomEntity GetById(int idRoom, int idHotel)
        {
            var param = new DynamicParameters();
            param.Add(name: "@ROOM_ID", value: idRoom, dbType: DbType.Int32, size: int.MaxValue, direction: ParameterDirection.Input);
            param.Add(name: "@HOTEL_ID", value: idHotel, dbType: DbType.Int32, size: int.MaxValue, direction: ParameterDirection.Input);
            param.Add(name: "@OUT_DATA", dbType: DbType.String, size: int.MaxValue, direction: ParameterDirection.Output);

            _connection.Execute(
                $"{name_db}.[dbo].[ROOM_GETBY_ID]",
                param,
                commandType: CommandType.StoredProcedure,
                transaction: _transaction);

            RoomRecord result = new RoomRecord();
            if (!string.IsNullOrEmpty(param.Get<string>("@OUT_DATA")))
            {
                result = JsonConvert.DeserializeObject<RoomRecord>(param.Get<string>("@OUT_DATA"));
                return RoomEntity.CreateFromRecord(result);
            }

            return RoomEntity.NoExistsRecord(0);
        }

        public List<RoomFilterEntity> FindByFilters(string city, DateTime init, DateTime end)
        {
            var param = new DynamicParameters();
            param.Add(name: "@CITY", city, dbType: DbType.String, size: 50, direction: ParameterDirection.Input);
            param.Add(name: "@CHECK_IN", init, dbType: DbType.DateTime, direction: ParameterDirection.Input);
            param.Add(name: "@CHECK_OUT", end, dbType: DbType.DateTime, direction: ParameterDirection.Input);
            param.Add(name: "@OUT_DATA", dbType: DbType.String, size: int.MaxValue, direction: ParameterDirection.Output);

            _connection.Execute(
                $"{name_db}.[dbo].[ROOM_FINDBY_FILTERS]",
                param,
                commandType: CommandType.StoredProcedure,
                transaction: _transaction);

            List<RoomFilterRecord> result = new List<RoomFilterRecord>();
            if (!string.IsNullOrEmpty(param.Get<string>("@OUT_DATA")))
            {
                result = JsonConvert.DeserializeObject<List<RoomFilterRecord>>(param.Get<string>("@OUT_DATA"));
                return result.Select(RoomFilterEntity.CreateFromRecord).ToList();
            }

            return new List<RoomFilterEntity>();
        }

        public int Create(string json)
        {
            var parameters = new DynamicParameters();
            parameters.Add(name: "@IN_DATA", json, dbType: DbType.String, size: int.MaxValue, direction: ParameterDirection.Input);
            parameters.Add(name: "@OUT_ID", dbType: DbType.Int32, size: 100, direction: ParameterDirection.Output);
            parameters.Add(name: "@OUT_ERROR_MESSAGE", dbType: DbType.String, size: 4000, direction: ParameterDirection.Output);

            _connection.Execute($"{name_db}.[dbo].[ROOM_INSERT]",
                                parameters,
                                commandType: CommandType.StoredProcedure,
                                transaction: _transaction);

            if (!string.IsNullOrEmpty(parameters.Get<string>("@OUT_ERROR_MESSAGE")))
                throw new ApiException(parameters.Get<string>("@OUT_ERROR_MESSAGE"));

            return parameters.Get<int>("@OUT_ID");
        }

        public bool UpdateRoom(string json)
        {
            var parameters = new DynamicParameters();
            parameters.Add(name: "@IN_DATA", json, dbType: DbType.String, size: int.MaxValue, direction: ParameterDirection.Input);
            parameters.Add(name: "@OUT_CONFIRM", dbType: DbType.Boolean, size: 1, direction: ParameterDirection.Output);
            parameters.Add(name: "@OUT_ERROR_MESSAGE", dbType: DbType.String, size: 4000, direction: ParameterDirection.Output);

            _connection.Execute($"{name_db}.[dbo].[ROOM_UPDATE]",
                                parameters,
                                commandType: CommandType.StoredProcedure,
                                transaction: _transaction);

            if (!string.IsNullOrEmpty(parameters.Get<string>("@OUT_ERROR_MESSAGE")))
                throw new ApiException(parameters.Get<string>("@OUT_ERROR_MESSAGE"));

            return parameters.Get<bool>("@OUT_CONFIRM");
        }

        public void UpdateStateRoom(int id, bool isActive)
        {
            var parameters = new DynamicParameters();
            parameters.Add(name: "@ROOM_ID", id, dbType: DbType.Int32, size: int.MaxValue, direction: ParameterDirection.Input);
            parameters.Add(name: "@IS_ACTIVE", isActive, dbType: DbType.Boolean, size: int.MaxValue, direction: ParameterDirection.Input);
            parameters.Add(name: "@OUT_CONFIRM", dbType: DbType.Boolean, size: 1, direction: ParameterDirection.Output);
            parameters.Add(name: "@OUT_ERROR_MESSAGE", dbType: DbType.String, size: 4000, direction: ParameterDirection.Output);

            _connection.Execute($"{name_db}.[dbo].[ROOM_UPDATE_ISACTIVE]",
                                parameters,
                                commandType: CommandType.StoredProcedure,
                                transaction: _transaction);

            if (!string.IsNullOrEmpty(parameters.Get<string>("@OUT_ERROR_MESSAGE")))
                throw new ApiException(parameters.Get<string>("@OUT_ERROR_MESSAGE"));
        }
    }
}
