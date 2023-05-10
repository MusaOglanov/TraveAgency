using Microsoft.AspNetCore.Identity;
using System.Collections;
using System.Collections.Generic;

namespace TraveAgency.Models
{
    public class AppUser:IdentityUser
    {
        public string Name { get; set; }
        public string SurName { get; set; }
        public bool IsDeactive { get; set; }
        public bool IsRemember { get; set; }
        public ICollection<Income> Incomes { get; set; }
    }
}
