using Api.Manager.Base.Entity;
using Api.Manager.Domain.Records;

namespace Api.Manager.Domain.Entity
{
    public class RoomFilterEntity : Entidad<RoomFilterEntity>
    {
        public RoomFilterEntity(int id) : base(id)
        {
            RoomId = id;
        }

        public int HotelId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int RoomId { get; set; }
        public string Number { get; set; }
        public TypeRoomEntity TypeRoom { get; set; }
        public decimal BaseCost { get; set; }
        public decimal Taxes { get; set; }
        public string Location { get; set; }

        public static RoomFilterEntity CreateFromRecord(RoomFilterRecord record)
        {
            if (record == null) throw new ArgumentNullException(nameof(record));

            return new RoomFilterEntity(record.RoomId)
            {
                HotelId = record.HotelId,
                Name = record.Name,
                Address = record.Address,
                Country = record.Country,
                State = record.State,
                City = record.City,
                Phone = record.Phone,
                Email = record.Email,
                RoomId = record.RoomId,
                Number = record.Number,
                TypeRoom = new TypeRoomEntity(record.TypeRoom.TypeID)
                {
                    Id = record.TypeRoom.TypeID,
                    Name = record.TypeRoom.NameType,
                    NumberOfGuests = record.TypeRoom.NumberOfGuests
                },
                BaseCost = record.BaseCost,
                Taxes = record.Taxes,
                Location = record.Location,
                ExistsInDB = true
            };
        }

        public static RoomFilterEntity NoExistsRecord(int id)
        {
            return new RoomFilterEntity(id) { ExistsInDB = false };
        }
    }
}
