using Api.Manager.Base.Entity;
using Api.Manager.Domain.Records;

namespace Api.Manager.Domain.Entity
{
    public class UserEntity : Entidad<UserEntity>
    {
        public UserEntity(int id) : base(id)
        {
            UserID = id;
        }

        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime RegistrationDate { get; set; }
        public bool IsActive { get; set; }

        public static UserEntity CreateFromRecord(UserRecord record)
        {
            if (record == null) throw new ArgumentNullException(nameof(record));

            return new UserEntity(record.UserID)
            {
                UserID = record.UserID,
                UserName = record.UserName,
                Password = record.PasswordHash,
                Email = record.Email,
                FirstName = record.FirstName,
                LastName = record.LastName,
                RegistrationDate = record.RegistrationDate,
                IsActive = record.IsActive,
                ExistsInDB = true
            };
        }

        public static UserEntity NoExistsRecord(int id)
        {
            return new UserEntity(id) { ExistsInDB = false };
        }
    }
}
