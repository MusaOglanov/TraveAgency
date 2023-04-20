using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TraveAgency.Models
{
    public class HotelRoomType
    {
        public int Id { get; set; }
        [Required]

        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public Hotel Hotel { get; set; }
        public int HotelId { get; set; }
        public bool IsDeactive { get; set; }
    }
}
