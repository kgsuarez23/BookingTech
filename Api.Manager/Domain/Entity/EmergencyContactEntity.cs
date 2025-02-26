using Api.Manager.Base.Entity;
using Api.Manager.Domain.Records;

namespace Api.Manager.Domain.Entity
{
    public class EmergencyContactEntity : Entidad<EmergencyContactEntity>
    {
        public EmergencyContactEntity(int id) : base(id)
        {
            Id = id;
        }

        public int Id { get; set; }
        public int BookingID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ContactPhone { get; set; }

        public static EmergencyContactEntity CreateFromRecord(GuestRecord record)
        {
            if (record == null) throw new ArgumentNullException(nameof(record));

            return new EmergencyContactEntity(record.ReservationID)
            {
                Id = record.ReservationID,
                BookingID = record.ReservationID,
                FirstName = record.FirstName,
                LastName = record.LastName,
                ContactPhone = record.ContactPhone,
                ExistsInDB = true
            };
        }

        public static EmergencyContactEntity NoExistsRecord(int id)
        {
            return new EmergencyContactEntity(id) { ExistsInDB = false };
        }
    }
}
