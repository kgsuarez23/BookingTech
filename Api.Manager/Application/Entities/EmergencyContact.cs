using Api.Manager.Domain.Entity;

namespace Api.Manager.Application.Entities
{
    /// <summary>
    /// Data transfer object representing an emergency contact associated with a booking.
    /// </summary>
    public class EmergencyContactDto
    {
        /// <summary>
        /// Gets or sets the unique identifier for the emergency contact.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the booking associated with the emergency contact.
        /// </summary>
        public int BookingID { get; set; }

        /// <summary>
        /// Gets or sets the first name of the emergency contact.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name of the emergency contact.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the contact phone number of the emergency contact.
        /// </summary>
        public string ContactPhone { get; set; }

        public static EmergencyContactDto MapFrom(EmergencyContactEntity emer)
        {
            return new EmergencyContactDto
            {
                Id = emer.Id,
                BookingID = emer.BookingID,
                FirstName = emer.FirstName,
                LastName = emer.LastName,
                ContactPhone = emer.ContactPhone,
            };
        }
    }
}
