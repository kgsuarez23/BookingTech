namespace Api.Manager.Domain.Records
{
    public record GuestRecord
    {
        public int GuestID { get; set; }
        public int ReservationID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; }
        public string DocumentType { get; set; }
        public string DocumentNumber { get; set; }
        public string Email { get; set; }
        public string ContactPhone { get; set; }
    }
}
