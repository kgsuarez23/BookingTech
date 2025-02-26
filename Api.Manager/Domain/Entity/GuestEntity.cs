using Api.Manager.Base.Entity;
using Api.Manager.Domain.Records;

namespace Api.Manager.Domain.Entity
{
    public class GuestEntity : Entidad<GuestEntity>
    {
        public GuestEntity(int id) : base(id)
        {
            Id = id;
        }

        public int Id { get; set; }
        public int BookingID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; }
        public string DocumentType { get; set; }
        public string DocumentNumber { get; set; }
        public string Email { get; set; }
        public string ContactPhone { get; set; }

        public static GuestEntity CreateFromRecord(GuestRecord record)
        {
            if (record == null) throw new ArgumentNullException(nameof(record));

            return new GuestEntity(record.ReservationID)
            {
                Id = record.ReservationID,
                BookingID = record.ReservationID,
                FirstName = record.FirstName,
                LastName = record.LastName,
                BirthDate = record.BirthDate,
                Gender = record.Gender,
                DocumentType = record.DocumentType,
                DocumentNumber = record.DocumentNumber,
                Email = record.Email,
                ContactPhone = record.ContactPhone,
                ExistsInDB = true
            };
        }

        public static GuestEntity NoExistsRecord(int id)
        {
            return new GuestEntity(id) { ExistsInDB = false };
        }
    }
}
