using Api.Manager.Base.Entity;
using Api.Manager.Domain.Records;

namespace Api.Manager.Domain.Entity
{
    public class UserRoleEntity : Entidad<UserRoleEntity>
    {
        public UserRoleEntity(int id) : base(id)
        {
            UserID = id;
        }

        public int UserID { get; set; }
        public int RoleID { get; set; }
        public string RoleName { get; set; }
        public bool IsActive { get; set; }

        public static UserRoleEntity CreateFromRecord(UserRoleRecord record)
        {
            if (record == null) throw new ArgumentNullException(nameof(record));

            return new UserRoleEntity(record.UserID)
            {
                UserID = record.UserID,
                RoleID = record.RoleID,
                RoleName = record.RoleName,
                IsActive = record.IsActive,
                ExistsInDB = true
            };
        }

        public static UserRoleEntity NoExistsRecord(int id)
        {
            return new UserRoleEntity(id) { ExistsInDB = false };
        }
    }
}
