namespace Reto.API.Models.Request.Person
{
    public class UpdatePersonRequest
    {
        public string? Name { get; set; }
        public string? Gender { get; set; }
        public int? Age { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
