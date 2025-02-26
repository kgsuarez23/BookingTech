using Api.Manager.Domain.Entity;
using Api.Manager.Domain.Interfaces;
using Api.Manager.Domain.Records;
using Dapper;
using System.Data;

namespace Api.Manager.Infraestructure.Dbo
{
    public class RoleRepository : IRoleRepository
    {

        private readonly IDbConnection _connection;
        private readonly IDbTransaction _transaction;

        private readonly string name_db = "DB_Hotel";

        public RoleRepository(IDbConnection connection, IDbTransaction transaction)
        {
            _connection = connection;
            _transaction = transaction;
        }

        public List<RoleEntity> GetAllTypeRoleUserQuery()
        {
            var records = _connection.Query<RoleRecord>(
                $"{name_db}.[dbo].[ROLE_GET_ALL]",
                commandType: CommandType.StoredProcedure,
                transaction: _transaction);

            return records.Select(RoleEntity.CreateFromRecord).ToList();
        }
    }
}
