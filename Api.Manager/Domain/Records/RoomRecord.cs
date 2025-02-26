namespace Api.Manager.Domain.Records
{
    public record RoomRecord
    {
        public int RoomId { get; set; }
        public int HotelId { get; set; }
        public string Number { get; set; }
        public TypeRoomRecord TypeRoom { get; set; }
        public decimal BaseCost { get; set; }
        public decimal Taxes { get; set; }
        public string Location { get; set; }
        public bool IsActive { get; set; }
    }
}
