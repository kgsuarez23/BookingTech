namespace Api.Manager.Domain.Records
{
    public class RoomFilterRecord
    {
        public int HotelId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int RoomId { get; set; }
        public string Number { get; set; }
        public TypeRoomRecord TypeRoom { get; set; }
        public decimal BaseCost { get; set; }
        public decimal Taxes { get; set; }
        public string Location { get; set; }
    }
}
