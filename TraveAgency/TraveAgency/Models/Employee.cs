using System;

namespace TraveAgency.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public double Salary { get; set; }
        public long Mobile { get; set; }
        public string Email { get; set; }
        public DateTime Bithdate { get; set; }
        public DateTime HireDate { get; set; }
        public Position Position { get; set; }
        public int PositionId { get; set; }
        public string  Notes { get; set; }
        public bool  IsDeactive { get; set; }

    }
}
