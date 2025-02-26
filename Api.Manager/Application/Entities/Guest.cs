using Api.Manager.Domain.Entity;

namespace Api.Manager.Application.Entities
{
    /// <summary>
    /// Data transfer object representing a guest associated with a booking.
    /// </summary>
    public class GuestDto
    {
        /// <summary>
        /// Gets or sets the unique identifier for the guest.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the booking associated with the guest.
        /// </summary>
        public int BookingID { get; set; }

        /// <summary>
        /// Gets or sets the first name of the guest.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name of the guest.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the birth date of the guest.
        /// </summary>
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// Gets or sets the gender of the guest.
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// Gets or sets the type of identification document for the guest.
        /// </summary>
        public string DocumentType { get; set; }

        /// <summary>
        /// Gets or sets the number of the identification document for the guest.
        /// </summary>
        public string DocumentNumber { get; set; }

        /// <summary>
        /// Gets or sets the email address of the guest.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the contact phone number of the guest.
        /// </summary>
        public string ContactPhone { get; set; }

        public static GuestDto MapFrom(GuestEntity guest)
        {
            return new GuestDto
            {
                Id = guest.Id,
                BookingID = guest.BookingID,
                FirstName = guest.FirstName,
                LastName = guest.LastName,
                BirthDate = guest.BirthDate,
                Gender = guest.Gender,
                DocumentType = guest.DocumentType,
                DocumentNumber = guest.DocumentNumber,
                Email = guest.Email,
                ContactPhone = guest.ContactPhone,
            };
        }
    }
}
