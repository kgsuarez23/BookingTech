using Api.Manager.Base.Entity;
using Api.Manager.Domain.Records;

namespace Api.Manager.Domain.Entity
{
    public class BookingEntity : Entidad<BookingEntity>
    {
        public BookingEntity(int id) : base(id)
        {
            Id = id;
        }

        public int Id { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int NumberOfGuests { get; set; }
        public bool IsActive { get; set; }
        public HotelEntity InfoHotel { get; set; }
        public List<RoomEntity> InfoRooms { get; set; }
        public List<GuestEntity> InfoGuests { get; set; }
        public EmergencyContactEntity InfoEmergencyContact { get; set; }

        public static BookingEntity CreateFromRecord(BookingRecord record)
        {
            if (record == null) throw new ArgumentNullException(nameof(record));

            return new BookingEntity(record.ReservationID)
            {
                Id = record.ReservationID,
                CheckInDate = record.CheckInDate,
                CheckOutDate = record.CheckOutDate,
                NumberOfGuests = record.NumberOfGuests,
                IsActive = record.ReservationStatus,
                InfoRooms = record.Rooms.Select(rec => new RoomEntity(rec.RoomId)
                {
                    Id = rec.RoomId,
                    HotelId = rec.HotelId,
                    Number = rec.Number,
                    TypeRoom = new TypeRoomEntity(rec.TypeRoom.TypeID)
                    {
                        Id = rec.TypeRoom.TypeID,
                        Name = rec.TypeRoom.NameType,
                        NumberOfGuests = rec.TypeRoom.NumberOfGuests
                    },
                    BaseCost = rec.BaseCost,
                    Taxes = rec.Taxes,
                    Location = rec.Location,
                    IsActive = rec.IsActive
                }).ToList(),
                InfoHotel = new HotelEntity(record.HotelID)
                {
                    Id = record.HotelID,
                    Name = record.HotelName,
                    Address = record.HotelAddress,
                    Country = record.HotelCountry,
                    State = record.HotelState,
                    City = record.HotelCity,
                    Phone = record.HotelPhone,
                    Email = record.HotelEmail,
                    IsActive = record.HotelIsActive
                },
                InfoGuests = record.Guests.Select(rec => new GuestEntity(rec.GuestID)
                {
                    Id = rec.GuestID,
                    BookingID = record.ReservationID,
                    FirstName = rec.FirstName,
                    LastName = rec.LastName,
                    BirthDate = rec.BirthDate,
                    Gender = rec.Gender,
                    DocumentType = rec.DocumentType,
                    DocumentNumber = rec.DocumentNumber,
                    Email = rec.Email,
                    ContactPhone = rec.ContactPhone
                }).ToList(),
                InfoEmergencyContact = new EmergencyContactEntity(record.EmergencyContact.ContactID)
                {
                    Id = record.EmergencyContact.ContactID,
                    BookingID = record.ReservationID,
                    FirstName = record.EmergencyContact.FirstName,
                    LastName = record.EmergencyContact.LastName,
                    ContactPhone = record.EmergencyContact.ContactPhone
                },
                ExistsInDB = true
            };
        }

        public static BookingEntity NoExistsRecord(int id)
        {
            return new BookingEntity(id) { ExistsInDB = false };
        }
    }
}
