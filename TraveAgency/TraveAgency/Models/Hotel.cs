using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TraveAgency.Models
{
    public class Hotel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public List<HotelImage> HotelImages { get; set; }
        public List<HotelHotelCategory> HotelHotelCategories { get; set; }
        public List<HotelRoomType> HotelRoomTypes { get; set; }
        public HotelDetail HotelDetail { get; set; }
        public int Star { get; set; }
        public double Price { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        [NotMapped]
        public List<IFormFile> Photo { get; set; }
        public bool IsDeactive { get; set; }
        public Hotel()
        {
            Country = "Azərbaycan";
        }
    }
}
