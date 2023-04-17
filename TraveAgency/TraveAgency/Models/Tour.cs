using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TraveAgency.Models
{
    public class Tour
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string  Image { get; set; }
        public bool IsDomestic { get; set; }
        public int  Duration { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public int Adults { get; set; }
        public int? Children { get; set; }
        public TourCategory TourCategory { get; set; }
        public int TourCategoryId { get; set; }
        public List<TourHotel> TourHotels { get; set; }
        public string TourDescription { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }
        public bool IsDeactive { get; set; }
    }
}
