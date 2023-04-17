using System.Collections.Generic;

namespace TraveAgency.Models
{
    public class TourCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Tour> Tours { get; set; }
        public bool IsDeactive { get; set; }

    }
}
