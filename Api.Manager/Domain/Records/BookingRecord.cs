namespace Api.Manager.Domain.Records
{
    public record BookingRecord
    {
        //public int ReservationID { get; set; }
        //public int RoomID { get; set; }
        //public DateTime CheckInDate { get; set; }
        //public DateTime CheckOutDate { get; set; }
        //public int NumberOfGuests { get; set; }
        //public bool ReservationStatus { get; set; }

        public int ReservationID { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int NumberOfGuests { get; set; }
        public bool ReservationStatus { get; set; }

        //public int RoomID { get; set; }
        //public string RoomNumber { get; set; }
        //public int TypeID { get; set; }
        //public decimal RoomBaseCost { get; set; }
        //public decimal RoomTaxes { get; set; }
        //public string RoomLocation { get; set; }
        //public bool RoomIsActive { get; set; }


        public int HotelID { get; set; }
        public string HotelName { get; set; }
        public string HotelAddress { get; set; }
        public string HotelCountry { get; set; }
        public string HotelState { get; set; }
        public string HotelCity { get; set; }
        public string HotelPhone { get; set; }
        public string HotelEmail { get; set; }
        public bool HotelIsActive { get; set; }

        public RoomRecord[] Rooms { get; set; }
        public GuestRecord[] Guests { get; set; }
        public EmergencyContactRecord EmergencyContact { get; set; }
    }
}


