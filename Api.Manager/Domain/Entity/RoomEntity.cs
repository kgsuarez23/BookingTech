using Api.Manager.Base.Entity;
using Api.Manager.Domain.Records;

namespace Api.Manager.Domain.Entity
{
    public class RoomEntity : Entidad<RoomEntity>
    {
        public RoomEntity(int id) : base(id)
        {
            Id = id;
        }

        public int Id { get; set; }
        public int HotelId { get; set; }
        public string Number { get; set; }
        public TypeRoomEntity TypeRoom { get; set; }
        public decimal BaseCost { get; set; }
        public decimal Taxes { get; set; }
        public string Location { get; set; }
        public bool IsActive { get; set; }

        public static RoomEntity CreateFromRecord(RoomRecord record)
        {
            if (record == null) throw new ArgumentNullException(nameof(record));

            return new RoomEntity(record.RoomId)
            {
                Id = record.RoomId,
                HotelId = record.HotelId,
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
                IsActive = record.IsActive,
                ExistsInDB = true
            };
        }

        public static RoomEntity NoExistsRecord(int id)
        {
            return new RoomEntity(id) { ExistsInDB = false };
        }
    }
}
