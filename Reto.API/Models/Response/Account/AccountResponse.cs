namespace Reto.API.Models.Response.Account
{
    public class AccountResponse
    {
        public int AccountId { get; set; }
        public string AccountNumber { get; set; }
        public string AccountType { get; set; }
        public decimal InitialBalance { get; set; }
        public bool Status { get; set; }
        public int ClientId { get; set; }
        public string ClientName { get; set; }
    }
}
