using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TraveAgency.Models
{
    public class HotelDetail
    {
        public int Id { get; set; }
        public bool IsDomestic { get; set; }
        [Required]
        public DateTime CheckInTime { get; set; }
        [Required]
        public DateTime CheckOutTime { get; set; }
        public string Info { get; set; }
        public string Adress { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DataType(DataType.PhoneNumber)]
        [MaxLength(10)]
        public long PhoneNumber { get; set; }
        public string WebSite { get; set; }
        public double Rating { get; set; }
        public bool RoomAvailable { get; set; } 
        public Hotel Hotel { get; set; }
        [ForeignKey("Hotel")]
        public int HotelId { get; set; }
    }
}
