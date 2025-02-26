using Api.Manager.Domain.Entity;
using Api.Manager.Domain.Exceptions;
using Api.Manager.Domain.Interfaces;
using Api.Manager.Domain.Records;
using Dapper;
using System.Data;

namespace Api.Manager.Infraestructure.Dbo
{
    public class HotelRepository : IHotelRepository
    {
        private readonly IDbConnection _connection;
        private readonly IDbTransaction _transaction;

        private readonly string name_db = "DB_Hotel";

        public HotelRepository(IDbConnection connection, IDbTransaction transaction)
        {
            _connection = connection;
            _transaction = transaction;
        }

        public List<HotelEntity> GetAll()
        {
            var records = _connection.Query<HotelRecord>(
                $"{name_db}.[dbo].[HOTEL_GET_ALL]",
                commandType: CommandType.StoredProcedure,
                transaction: _transaction);

            return records.Select(HotelEntity.CreateFromRecord).ToList();
        }

        public HotelEntity GetById(int id)
        {
            var param = new DynamicParameters();
            param.Add(name: "@ID_HOTEL", value: id, dbType: DbType.Int32, size: int.MaxValue, direction: ParameterDirection.Input);

            var records = _connection.Query<HotelRecord>(
                $"{name_db}.[dbo].[HOTEL_GETBY_ID]",
                param,
                commandType: CommandType.StoredProcedure,
                transaction: _transaction).SingleOrDefault();

            return records != null ? HotelEntity.CreateFromRecord(records) : HotelEntity.NoExistsRecord(0);
        }

        public int CreateHotel(string json)
        {
            var parameters = new DynamicParameters();
            parameters.Add(name: "@IN_DATA", json, dbType: DbType.String, size: int.MaxValue, direction: ParameterDirection.Input);
            parameters.Add(name: "@OUT_ID", dbType: DbType.Int32, size: 100, direction: ParameterDirection.Output);
            parameters.Add(name: "@OUT_ERROR_MESSAGE", dbType: DbType.String, size: 4000, direction: ParameterDirection.Output);

            _connection.Execute($"{name_db}.[dbo].[HOTEL_INSERT]",
                                parameters,
                                commandType: CommandType.StoredProcedure,
                                transaction: _transaction);

            if (!string.IsNullOrEmpty(parameters.Get<string>("@OUT_ERROR_MESSAGE")))
                throw new ApiException(parameters.Get<string>("@OUT_ERROR_MESSAGE"));

            return parameters.Get<int>("@OUT_ID");
        }

        public bool UpdateHotel(string json)
        {
            var parameters = new DynamicParameters();
            parameters.Add(name: "@IN_DATA", json, dbType: DbType.String, size: int.MaxValue, direction: ParameterDirection.Input);
            parameters.Add(name: "@OUT_CONFIRM", dbType: DbType.Boolean, size: 1, direction: ParameterDirection.Output);
            parameters.Add(name: "@OUT_ERROR_MESSAGE", dbType: DbType.String, size: 4000, direction: ParameterDirection.Output);

            _connection.Execute($"{name_db}.[dbo].[HOTEL_UPDATE]",
                                parameters,
                                commandType: CommandType.StoredProcedure,
                                transaction: _transaction);

            if (!string.IsNullOrEmpty(parameters.Get<string>("@OUT_ERROR_MESSAGE")))
                throw new ApiException(parameters.Get<string>("@OUT_ERROR_MESSAGE"));

            return parameters.Get<bool>("@OUT_CONFIRM");
        }

        public void UpdateStateHotel(int id, bool isActive)
        {
            var parameters = new DynamicParameters();
            parameters.Add(name: "@HOTEL_ID", id, dbType: DbType.Int32, size: int.MaxValue, direction: ParameterDirection.Input);
            parameters.Add(name: "@IS_ACTIVE", isActive, dbType: DbType.Boolean, size: int.MaxValue, direction: ParameterDirection.Input);
            parameters.Add(name: "@OUT_CONFIRM", dbType: DbType.Boolean, size: 1, direction: ParameterDirection.Output);
            parameters.Add(name: "@OUT_ERROR_MESSAGE", dbType: DbType.String, size: 4000, direction: ParameterDirection.Output);

            _connection.Execute($"{name_db}.[dbo].[HOTEL_UPDATE_ISACTIVE]",
                                parameters,
                                commandType: CommandType.StoredProcedure,
                                transaction: _transaction);

            if (!string.IsNullOrEmpty(parameters.Get<string>("@OUT_ERROR_MESSAGE")))
                throw new ApiException(parameters.Get<string>("@OUT_ERROR_MESSAGE"));
        }
    }
}
