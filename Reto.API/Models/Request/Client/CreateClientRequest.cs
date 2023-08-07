using System.ComponentModel.DataAnnotations;

namespace Reto.API.Models.Request.Client
{
    public class CreateClientRequest
    {
        [Required]
        public string Password { get; set; }
        
        [Required]
        public int PersonId { get; set; }
    }
}
