using System;
using System.ComponentModel.DataAnnotations;

namespace TraveAgency.Models
{
    public class Customer
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }

        [Required]
        [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Geçerli bir telefon numarası giriniz.")]
        public string Mobile { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        public string City { get; set; }
        [Required]
        public string Country { get; set; }
        public string Adress { get; set; }

        public DateTime RegistrationDate { get; set; }
    }
}
