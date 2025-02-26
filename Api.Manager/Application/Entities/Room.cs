using Api.Manager.Domain.Entity;

namespace Api.Manager.Application.Entities
{
    /// <summary>
    /// Data transfer object representing a room, including its details and associated room type information.
    /// </summary>
    public class RoomDto
    {
        /// <summary>
        /// Gets or sets the unique identifier for the room.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the hotel to which the room belongs.
        /// </summary>
        public int HotelId { get; set; }

        /// <summary>
        /// Gets or sets the room number.
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Gets or sets the type of the room.
        /// </summary>
        public TypeRoomDto TypeRoom { get; set; }

        /// <summary>
        /// Gets or sets the base cost of the room before applying taxes.
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

        /// <summary>
        /// Gets or sets a value indicating whether the room is currently active.
        /// </summary>
        public bool IsActive { get; set; }

        public static RoomDto MapFrom(RoomEntity room)
        {
            return new RoomDto
            {
                Id = room.Id,
                HotelId = room.HotelId,
                Number = room.Number,
                TypeRoom = new TypeRoomDto()
                {
                    Id = room.TypeRoom.Id,
                    Name = room.TypeRoom.Name,
                    NumberOfGuests = room.TypeRoom.NumberOfGuests
                },
                BaseCost = room.BaseCost,
                Taxes = room.Taxes,
                Location = room.Location,
                IsActive = room.IsActive
            };
        }
    }
}
