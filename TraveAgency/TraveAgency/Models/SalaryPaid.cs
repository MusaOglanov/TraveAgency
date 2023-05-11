using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace TraveAgency.Models
{
    public class SalaryPaid
    {
        public int Id { get; set; }

        public Employee Employee { get; set; }
        public int EmployeeId { get; set; }

        public decimal Money { get; set; }
        [Required]
        public string About { get; set; }
        public DateTime CreateTime { get; set; }
        public Kassa Kassa { get; set; }
        public int KassaId { get; set; }
        public AppUser AppUser { get; set; }
        public string AppUserId { get; set; }
    }
}
