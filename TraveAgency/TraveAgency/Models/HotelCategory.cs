using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TraveAgency.Models
{
    public class HotelCategory
    {
        public int Id { get; set; }
        [Required]

        public string Name { get; set; }
        public List<HotelHotelCategory> HotelHotelCategories { get; set; }
        public bool IsDeactive { get; set;}
        public HotelCategory()
        {
            Name = "Azərbaycan";
        }
    }
}
