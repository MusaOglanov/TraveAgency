using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TraveAgency.Models
{
    public class Airport
    {
        public int Id { get; set; }
        [Required]

        public string Name { get; set; }
        [Required]

        public string Country { get; set; }
        [Required]

        public string City { get; set; }
        [MaxLength(3)]
        public string Code { get; set; }
        public bool IsDeactive { get; set; }
        [NotMapped]
        public List<AirlineTicket> AirlineTickets { get; set; }
    }
}
