using Academy2018_.NET_Homework5.Infrastructure.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace Academy2018_.NET_Homework5.Infrastructure.Models
{
    public class Ticket : IEntity
    {
        [Key]
        public int Id { get; set; }

        public decimal Price { get; set; }

        public string FlightNumber { get; set; }
    }
}
