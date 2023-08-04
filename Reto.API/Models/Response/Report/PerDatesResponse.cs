namespace Reto.API.Models.Response.Report
{
    public class PerDatesResponse
    {
        //public DateTime CreatedDate { get; set; }
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public int AccountId { get; set; }
        public string AccountNumber { get; set; }
        public string AccountType { get; set; }
        public decimal InitialBalance { get; set; }
        public bool Status { get; set; }
        public decimal TotalDebits { get; set; }
        public decimal TotalCredits { get; set; }
        public decimal CurrentBalance { get; set; }
    }
}
