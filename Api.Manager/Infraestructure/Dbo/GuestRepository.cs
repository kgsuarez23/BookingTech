using Api.Manager.Domain.Entity;
using Api.Manager.Domain.Exceptions;
using Api.Manager.Domain.Interfaces;
using Api.Manager.Domain.Records;
using Dapper;
using System.Data;

namespace Api.Manager.Infraestructure.Dbo
{
    public class GuestRepository : IGuestRepository
    {
        private readonly IDbConnection _connection;
        private readonly IDbTransaction _transaction;

        private readonly string name_db = "DB_Hotel";

        public GuestRepository(IDbConnection connection, IDbTransaction transaction)
        {
            _connection = connection;
            _transaction = transaction;
        }

        public void CreateGuest(string json, int booking)
        {
            var param = new DynamicParameters();
            param.Add(name: "@IN_DATA", json, dbType: DbType.String, size: int.MaxValue, direction: ParameterDirection.Input);
            param.Add(name: "@BOOKING_ID", booking, dbType: DbType.Int32, size: int.MaxValue, direction: ParameterDirection.Input);
            param.Add(name: "@OUT_ERROR_MESSAGE", dbType: DbType.String, size: 4000, direction: ParameterDirection.Output);

            _connection.Execute(
                $"{name_db}.[dbo].[GUEST_INSERT]",
                param,
                commandType: CommandType.StoredProcedure,
                transaction: _transaction);

            if (!string.IsNullOrEmpty(param.Get<string>("OUT_ERROR_MESSAGE")))
                throw new ApiException(param.Get<string>("OUT_ERROR_MESSAGE"));
        }

        public List<GuestEntity> GetAll()
        {
            var records = _connection.Query<GuestRecord>(
                $"{name_db}.[dbo].[GUEST_GET_ALL]",
                commandType: CommandType.StoredProcedure,
                transaction: _transaction);

            return records.Select(GuestEntity.CreateFromRecord).ToList();
        }
    }
}
