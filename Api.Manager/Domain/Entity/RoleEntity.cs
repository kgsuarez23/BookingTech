using Api.Manager.Base.Entity;
using Api.Manager.Domain.Records;

namespace Api.Manager.Domain.Entity
{
    public class RoleEntity : Entidad<RoleEntity>
    {
        public RoleEntity(int id) : base(id)
        {
            Id = id;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }

        public static RoleEntity CreateFromRecord(RoleRecord record)
        {
            if (record == null) throw new ArgumentNullException(nameof(record));

            return new RoleEntity(record.RoleID)
            {
                Id = record.RoleID,
                Name = record.RoleName,
                Description = record.Description,
                IsActive = record.IsActive,
                ExistsInDB = true
            };
        }

        public static RoleEntity NoExistsRecord(int id)
        {
            return new RoleEntity(id) { ExistsInDB = false };
        }
    }
}
