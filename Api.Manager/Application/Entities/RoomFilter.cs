using Api.Manager.Domain.Entity;

namespace Api.Manager.Application.Entities
{
    /// <summary>
    /// Data transfer object representing filter criteria for rooms,
    /// including related hotel details and room-specific attributes.
    /// </summary>
    public class RoomFilterDto
    {
        /// <summary>
        /// Gets or sets the unique identifier of the hotel.
        /// </summary>
        public int HotelId { get; set; }

        /// <summary>
        /// Gets or sets the name of the hotel.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the address of the hotel.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the country where the hotel is located.
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets the state or region where the hotel is located.
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// Gets or sets the city where the hotel is located.
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the contact phone number of the hotel.
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Gets or sets the contact email address of the hotel.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the room.
        /// </summary>
        public int RoomId { get; set; }

        /// <summary>
        /// Gets or sets the room number.
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Gets or sets the type details of the room.
        /// </summary>
        public TypeRoomDto TypeRoom { get; set; }

        /// <summary>
        /// Gets or sets the base cost of the room before taxes.
        /// </summary>
        public decimal BaseCost { get; set; }

        /// <summary>
        /// Gets or sets the taxes applicable to the room.
        /// </summary>
        public decimal Taxes { get; set; }

        /// <summary>
        /// Gets or sets the location details of the room.
        /// </summary>
        public string Location { get; set; }

        public static RoomFilterDto MapFrom(RoomFilterEntity room)
        {
            return new RoomFilterDto
            {

                HotelId = room.HotelId,
                Name = room.Name,
                Address = room.Address,
                Country = room.Country,
                State = room.State,
                City = room.City,
                Phone = room.Phone,
                Email = room.Email,
                RoomId = room.RoomId,
                Number = room.Number,
                TypeRoom = new TypeRoomDto()
                {
                    Id = room.TypeRoom.Id,
                    Name = room.TypeRoom.Name,
                    NumberOfGuests = room.TypeRoom.NumberOfGuests
                },
                BaseCost = room.BaseCost,
                Taxes = room.Taxes,
                Location = room.Location
            };
        }
    }
}
