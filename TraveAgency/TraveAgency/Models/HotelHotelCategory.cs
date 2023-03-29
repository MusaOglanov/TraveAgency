namespace TraveAgency.Models
{
    public class HotelHotelCategory
    {
        public int Id { get; set; }
        public Hotel Hotel { get; set; }
        public int HotelId { get; set; }
        public HotelCategory HotelCategory { get; set; }
        public int HotelCategoryId { get; set; }
    }
}
