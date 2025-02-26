using Api.Manager.Domain.Entity;

namespace Api.Manager.Application.Entities
{
    /// <summary>
    /// Data transfer object representing a room type, including its name and guest capacity.
    /// </summary>
    public class TypeRoomDto
    {
        /// <summary>
        /// Gets or sets the unique identifier for the room type.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the room type.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the maximum number of guests that can be accommodated in this room type.
        /// </summary>
        public int NumberOfGuests { get; set; }

        public static TypeRoomDto MapFrom(TypeRoomEntity type)
        {
            return new TypeRoomDto
            {
                Id = type.Id,
                Name = type.Name,
                NumberOfGuests = type.NumberOfGuests
            };
        }
    }
}
