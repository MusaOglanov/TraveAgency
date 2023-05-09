using Microsoft.AspNetCore.Identity;

namespace TraveAgency.Models
{
    public class AppUser:IdentityUser
    {
        public string Name { get; set; }
        public string SurName { get; set; }
        public bool IsDeactive { get; set; }
        public bool IsRemember { get; set; }
    }
}
