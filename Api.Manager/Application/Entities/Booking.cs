using Api.Manager.Domain.Entity;

namespace Api.Manager.Application.Entities
{
    /// <summary>
    /// Data transfer object representing a booking, including associated hotel, room, guest, and emergency contact details.
    /// </summary>
    public class BookingDto
    {
        /// <summary>
        /// Gets or sets the unique identifier for the booking.
        /// </summary>
        public int Id { get; set; }

        // public int RoomId { get; set; } // Property commented out.

        /// <summary>
        /// Gets or sets the check-in date for the booking.
        /// </summary>
        public DateTime CheckInDate { get; set; }

        /// <summary>
        /// Gets or sets the check-out date for the booking.
        /// </summary>
        public DateTime CheckOutDate { get; set; }

        /// <summary>
        /// Gets or sets the number of guests included in the booking.
        /// </summary>
        public int NumberOfGuests { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the booking is active.
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets the hotel information associated with the booking.
        /// </summary>
        public HotelDto InfoHotel { get; set; }

        /// <summary>
        /// Gets or sets the list of room details included in the booking.
        /// </summary>
        public List<RoomDto> InfoRoom { get; set; }

        /// <summary>
        /// Gets or sets the list of guest details associated with the booking.
        /// </summary>
        public List<GuestDto> InfoGuests { get; set; }

        /// <summary>
        /// Gets or sets the emergency contact information for the booking.
        /// </summary>
        public EmergencyContactDto InfoEmergencyContact { get; set; }

        public static BookingDto MapFrom(BookingEntity booking)
        {
            return new BookingDto
            {
                Id = booking.Id,
                //RoomId = booking.RoomId,
                CheckInDate = booking.CheckInDate,
                CheckOutDate = booking.CheckOutDate,
                NumberOfGuests = booking.NumberOfGuests,
                IsActive = booking.IsActive,
                InfoRoom = booking.InfoRooms.Select(rec => new RoomDto()
                {
                    Id = rec.Id,
                    HotelId = rec.HotelId,
                    Number = rec.Number,
                    TypeRoom = new TypeRoomDto()
                    {
                        Id = rec.TypeRoom.Id,
                        Name = rec.TypeRoom.Name,
                        NumberOfGuests = rec.TypeRoom.NumberOfGuests
                    },
                    BaseCost = rec.BaseCost,
                    Taxes = rec.Taxes,
                    Location = rec.Location,
                    IsActive = rec.IsActive
                }).ToList(),
                InfoHotel = new HotelDto()
                {
                    Id = booking.InfoHotel.Id,
                    Name = booking.InfoHotel.Name,
                    Address = booking.InfoHotel.Address,
                    Country = booking.InfoHotel.Country,
                    State = booking.InfoHotel.State,
                    City = booking.InfoHotel.City,
                    Phone = booking.InfoHotel.Phone,
                    Email = booking.InfoHotel.Email,
                    IsActive = booking.InfoHotel.IsActive
                },
                InfoGuests = booking.InfoGuests.Select(rec => new GuestDto()
                {
                    Id = rec.Id,
                    BookingID = rec.BookingID,
                    FirstName = rec.FirstName,
                    LastName = rec.LastName,
                    BirthDate = rec.BirthDate,
                    Gender = rec.Gender,
                    DocumentType = rec.DocumentType,
                    DocumentNumber = rec.DocumentNumber,
                    Email = rec.Email,
                    ContactPhone = rec.ContactPhone
                }).ToList(),
                InfoEmergencyContact = new EmergencyContactDto()
                {
                    Id = booking.InfoEmergencyContact.Id,
                    BookingID = booking.InfoEmergencyContact.BookingID,
                    FirstName = booking.InfoEmergencyContact.FirstName,
                    LastName = booking.InfoEmergencyContact.LastName,
                    ContactPhone = booking.InfoEmergencyContact.ContactPhone
                }
            };
        }
    }
}
