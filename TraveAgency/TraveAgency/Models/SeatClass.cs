using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TraveAgency.Models
{
    public class SeatClass
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int SeatPrice { get; set; }
        [MaxLength(500)]
        public string Info { get; set; }
        public bool IsDeactive { get; set; }
        public ICollection<AirlineTicket> AirlineTicket { get; set; }

    }
}
