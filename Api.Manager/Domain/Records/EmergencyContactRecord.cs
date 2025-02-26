namespace Api.Manager.Domain.Records
{
    public record EmergencyContactRecord
    {
        public int ContactID { get; set; }
        public int ReservationID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ContactPhone { get; set; }
    }
}
