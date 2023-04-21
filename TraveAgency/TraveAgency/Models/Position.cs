using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TraveAgency.Models
{
    public class Position
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsDeactive { get; set; }
        public ICollection<Employee> Employees { get; set; }
    }
}
