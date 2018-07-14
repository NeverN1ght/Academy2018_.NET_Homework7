using Academy2018_.NET_Homework5.Infrastructure.Abstractions;

namespace Academy2018_.NET_Homework5.Infrastructure.Models
{
    public class Ticket : IEntity
    {
        public int Id { get; set; }

        public decimal Price { get; set; }

        public string FlightNumber { get; set; }
    }
}
