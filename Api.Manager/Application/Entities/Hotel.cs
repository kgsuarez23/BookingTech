using Api.Manager.Domain.Entity;

namespace Api.Manager.Application.Entities
{
    /// <summary>
    /// Data transfer object representing a hotel entity with its contact and location details.
    /// </summary>
    public class HotelDto
    {
        /// <summary>
        /// Gets or sets the unique identifier for the hotel.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the hotel.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the address of the hotel.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the city where the hotel is located.
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the state or region where the hotel is located.
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// Gets or sets the country where the hotel is located.
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets the contact phone number for the hotel.
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Gets or sets the contact email address for the hotel.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the hotel is active.
        /// </summary>
        public bool IsActive { get; set; }

        public static HotelDto MapFrom(HotelEntity hotel)
        {
            return new HotelDto
            {
                Id = hotel.Id,
                Name = hotel.Name,
                Address = hotel.Address,
                City = hotel.City,
                State = hotel.State,
                Country = hotel.Country,
                Phone = hotel.Phone,
                Email = hotel.Email,
                IsActive = hotel.IsActive
            };
        }
    }
}
