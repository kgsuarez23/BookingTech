using Api.Manager.Domain.Exceptions;
using Api.Manager.Domain.Interfaces;
using Dapper;
using System.Data;

namespace Api.Manager.Infraestructure.Dbo
{
    public class EmergencyContactRepository : IEmergencyContactRepository
    {

        private readonly IDbConnection _connection;
        private readonly IDbTransaction _transaction;

        private readonly string name_db = "DB_Hotel";

        public EmergencyContactRepository(IDbConnection connection, IDbTransaction transaction)
        {
            _connection = connection;
            _transaction = transaction;
        }

        public void Create(string json, int idBooking)
        {
            var param = new DynamicParameters();
            param.Add(name: "@IN_DATA", json, dbType: DbType.String, size: int.MaxValue, direction: ParameterDirection.Input);
            param.Add(name: "@BOOKING_ID", idBooking, dbType: DbType.Int32, size: int.MaxValue, direction: ParameterDirection.Input);
            param.Add(name: "@OUT_ERROR_MESSAGE", dbType: DbType.String, size: 4000, direction: ParameterDirection.Output);

            _connection.Execute(
                $"{name_db}.[dbo].[EMERGENCYCONTACT_INSERT]",
                param,
                commandType: CommandType.StoredProcedure,
                transaction: _transaction);

            if (!string.IsNullOrEmpty(param.Get<string>("OUT_ERROR_MESSAGE")))
                throw new ApiException(param.Get<string>("OUT_ERROR_MESSAGE"));
        }
    }
}
