namespace Reto.API.Models.Response.Transaction
{
    public class CreateTransactionResponse
    {
        public string AccountNumber { get; set; }
        public string AccountType { get; set; }
        public string TransactionType { get; set; }
        public decimal InitialBalance { get; set; }
        public DateTime CreatedDate { get; set; }
        public decimal Value { get; set; }
        public decimal Balance { get; set; }
        public int AccountId { get; set; }
    }
}
