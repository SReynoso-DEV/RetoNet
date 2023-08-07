namespace Reto.Application.Features.Person.Commands.AddUpdate
{
    public class PersonCommandQueryResponse
    {
        public int PersonId { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Identification { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
    }
}
