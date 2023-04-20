using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TraveAgency.Models
{
    public class TourCategory
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public List<Tour> Tours { get; set; }
        public bool IsDeactive { get; set; }

    }
}
