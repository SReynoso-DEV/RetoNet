using System.ComponentModel.DataAnnotations;

namespace Reto.API.Models.Request.Transaction
{
    public class CreateTransactionRequest
    {
        [Required]
        public DateTime? CreatedDate { get; set; }

        [Required]
        public string TransactionType { get; set; }

        [Required]
        public decimal Value { get; set; }

        [Required]
        public int AccountId { get; set; }

    }
}
