using System.ComponentModel.DataAnnotations;

namespace Reto.API.Models.Request.Person
{
    public class CreatePersonRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        public string Identification { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
    }
}
