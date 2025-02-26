using Api.Manager.Domain.Entity;

namespace Api.Manager.Domain.Interfaces
{
    public interface IUserRepository
    {
        bool ValidateCredentials(string username, string password);
        IEnumerable<UserEntity> ListUsers();
        UserEntity SearchUserByUserName(string userName);
        int RegisterUser(string json);
        IEnumerable<UserRoleEntity> SearchUserRolById(int id);
        bool AddRoleToUser(int userId, int roleId);
        bool UpdateRoleToUser(int userId, int roleId, bool state);
    }
}
