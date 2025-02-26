using Api.Manager.Domain.Entity;
using Api.Manager.Domain.Exceptions;
using Api.Manager.Domain.Interfaces;
using Api.Manager.Domain.Records;
using Dapper;
using System.Data;

namespace Api.Manager.Infraestructure.Dbo
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbConnection _connection;
        private readonly IDbTransaction _transaction;

        private readonly string name_db = "DB_Hotel";

        public UserRepository(IDbConnection connection, IDbTransaction transaction)
        {
            _connection = connection;
            _transaction = transaction;
        }

        public bool ValidateCredentials(string username, string password)
        {
            var parameters = new DynamicParameters();
            parameters.Add(name: "@USERNAME", username, dbType: DbType.String, size: 100, direction: ParameterDirection.Input);
            parameters.Add(name: "@HASH_PASSWORD", password, dbType: DbType.String, size: 100, direction: ParameterDirection.Input);
            parameters.Add(name: "@OUT", dbType: DbType.String, size: 100, direction: ParameterDirection.Output);

            _connection.Execute(
                $"{name_db}.[dbo].[USER_SEARCHID]",
                commandType: CommandType.StoredProcedure,
                transaction: _transaction);

            parameters.Get<string>("@OUT");

            return true;
        }

        public int RegisterUser(string json)
        {
            var param = new DynamicParameters();
            param.Add(name: "@IN_DATA", json, dbType: DbType.String, size: int.MaxValue, direction: ParameterDirection.Input);
            param.Add(name: "@OUT_ID", dbType: DbType.Int32, size: int.MaxValue, direction: ParameterDirection.Output);
            param.Add(name: "@OUT_ERROR_MESSAGE", dbType: DbType.String, size: 4000, direction: ParameterDirection.Output);

            _connection.Execute(
                $"{name_db}.[dbo].[USER_INSERT]",
                param,
                commandType: CommandType.StoredProcedure,
                transaction: _transaction);

            if (!string.IsNullOrEmpty(param.Get<string>("@OUT_ERROR_MESSAGE")))
                throw new ApiException(param.Get<string>("@OUT_ERROR_MESSAGE"));

            return param.Get<int>("@OUT_ID");
        }

        public IEnumerable<UserEntity> ListUsers()
        {
            var records = _connection.Query<UserRecord>(
                $"{name_db}.[dbo].[USER_LIST]",
                commandType: CommandType.StoredProcedure,
                transaction: _transaction);

            return records.Select(UserEntity.CreateFromRecord).ToList();
        }

        public UserEntity SearchUserByUserName(string userName)
        {
            var param = new DynamicParameters();
            param.Add(name: "@USER_NAME", userName, dbType: DbType.String, size: int.MaxValue, direction: ParameterDirection.Input);

            var records = _connection.Query<UserRecord>(
                $"{name_db}.[dbo].[USER_SEARCHBY_USERNAME]",
                param,
                commandType: CommandType.StoredProcedure,
                transaction: _transaction).SingleOrDefault();

            return records != null ? UserEntity.CreateFromRecord(records) : UserEntity.NoExistsRecord(0);
        }

        public IEnumerable<UserRoleEntity> SearchUserRolById(int id)
        {
            var param = new DynamicParameters();
            param.Add(name: "@USER_ID", id, dbType: DbType.Int32, size: int.MaxValue, direction: ParameterDirection.Input);

            var records = _connection.Query<UserRoleRecord>(
                $"{name_db}.[dbo].[USER_ROLE_SEARCHBY_ID]",
                param,
                commandType: CommandType.StoredProcedure,
                transaction: _transaction);

            return records.Select(UserRoleEntity.CreateFromRecord).ToList();
        }

        public bool AddRoleToUser(int userId, int roleId)
        {
            var param = new DynamicParameters();
            param.Add(name: "@USER_ID", userId, dbType: DbType.Int32, size: int.MaxValue, direction: ParameterDirection.Input);
            param.Add(name: "@ROLE_ID", roleId, dbType: DbType.Int32, size: int.MaxValue, direction: ParameterDirection.Input);
            param.Add(name: "@OUT_DATA", dbType: DbType.Boolean, size: int.MaxValue, direction: ParameterDirection.Output);
            param.Add(name: "@OUT_ERROR_MESSAGE", dbType: DbType.String, size: 4000, direction: ParameterDirection.Output);

            _connection.Execute(
                $"{name_db}.[dbo].[USER_ROLE_INSERT]",
                param,
                commandType: CommandType.StoredProcedure,
                transaction: _transaction);

            if (!string.IsNullOrEmpty(param.Get<string>("@OUT_ERROR_MESSAGE")))
                throw new ApiException(param.Get<string>("@OUT_ERROR_MESSAGE"));

            return param.Get<bool>("@OUT_DATA");
        }

        public bool UpdateRoleToUser(int userId, int roleId, bool state)
        {
            var param = new DynamicParameters();
            param.Add(name: "@USER_ID", userId, dbType: DbType.Int32, size: int.MaxValue, direction: ParameterDirection.Input);
            param.Add(name: "@ROLE_ID", roleId, dbType: DbType.Int32, size: int.MaxValue, direction: ParameterDirection.Input);
            param.Add(name: "@STATE", state, dbType: DbType.Boolean, direction: ParameterDirection.Input);
            param.Add(name: "@OUT_DATA", dbType: DbType.Boolean, size: int.MaxValue, direction: ParameterDirection.Output);
            param.Add(name: "@OUT_ERROR_MESSAGE", dbType: DbType.String, size: 4000, direction: ParameterDirection.Output);

            _connection.Execute(
                $"{name_db}.[dbo].[USER_ROLE_UPTADE]",
                param,
                commandType: CommandType.StoredProcedure,
                transaction: _transaction);

            if (!string.IsNullOrEmpty(param.Get<string>("@OUT_ERROR_MESSAGE")))
                throw new ApiException(param.Get<string>("@OUT_ERROR_MESSAGE"));

            return param.Get<bool>("@OUT_DATA");
        }
    }
}
