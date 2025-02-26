using Api.Manager.Domain.Entity;

namespace Api.Manager.Domain.Interfaces
{
    public interface IRoleRepository
    {
        List<RoleEntity> GetAllTypeRoleUserQuery();
    }
}
