using System.ComponentModel.DataAnnotations;

namespace Reto.API.Models.Request.Account
{
    public class CreateAccountRequest
    {
        [Required]
        public string AccountNumber { get; set; }

        [Required]
        public string AccountType { get; set; }

        [Required]
        public decimal InitialBalance { get; set; }

        [Required]
        public int ClientId { get; set; }
    }
}
