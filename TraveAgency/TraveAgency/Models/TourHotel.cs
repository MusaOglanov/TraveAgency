namespace TraveAgency.Models
{
    public class TourHotel
    {
        public int Id { get; set; }
        public Tour Tour { get; set; }
        public int TourId { get; set; }
        public Hotel Hotel { get; set; }
        public int HotelId { get; set; }
    }
}
