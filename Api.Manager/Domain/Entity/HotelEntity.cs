using Api.Manager.Base.Entity;
using Api.Manager.Domain.Records;

namespace Api.Manager.Domain.Entity
{
    public class HotelEntity : Entidad<HotelEntity>
    {
        public HotelEntity(int id) : base(id)
        {
            Id = id;
            Name = string.Empty;
            Address = string.Empty;
            City = string.Empty;
            State = string.Empty;
            Country = string.Empty;
            Phone = string.Empty;
            Email = string.Empty;
            IsActive = false;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }

        public static HotelEntity CreateFromRecord(HotelRecord record)
        {
            if (record == null) throw new ArgumentNullException(nameof(record));

            return new HotelEntity(record.HotelID)
            {
                Id = record.HotelID,
                Name = record.Name,
                Address = record.Address,
                City = record.City,
                State = record.State,
                Country = record.Country,
                Phone = record.Phone,
                Email = record.Email,
                IsActive = record.IsActive,
                ExistsInDB = true
            };
        }

        public static HotelEntity NoExistsRecord(int id)
        {
            return new HotelEntity(id) { ExistsInDB = false };
        }
    }
}
