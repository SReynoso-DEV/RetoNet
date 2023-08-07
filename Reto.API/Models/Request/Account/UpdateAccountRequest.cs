namespace Reto.API.Models.Request.Account
{
    public class UpdateAccountRequest
    {        
        public string? AccountNumber { get; set; }
        public string? AccountType { get; set; }
        public decimal? InitialBalance { get; set; }
        public bool? Status { get; set; }
    }
}
