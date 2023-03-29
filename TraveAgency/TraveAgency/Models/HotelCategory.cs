using System.Collections.Generic;

namespace TraveAgency.Models
{
    public class HotelCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<HotelHotelCategory> HotelHotelCategories { get; set; }
        public bool IsDeactive { get; set;}
      
    }
}
