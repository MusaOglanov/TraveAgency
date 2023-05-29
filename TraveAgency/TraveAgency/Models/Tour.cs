using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TraveAgency.Models
{
    public class Tour
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string  Image { get; set; }
        public double TourPrice { get; set; }

        public bool IsDomestic { get; set; }
        public int  Duration { get; set; }
        [Required]

        public DateTime StartDate { get; set; }
        [Required]

        public DateTime EndDate { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
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
