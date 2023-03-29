namespace TraveAgency.Models
{
    public class HotelImage
    {
        public int Id { get; set; }

        public string Image { get; set; }

        public Hotel Hotel { get; set; }

        public int HotelId { get; set; }
    }
}
