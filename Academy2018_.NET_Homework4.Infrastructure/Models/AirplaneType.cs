using Academy2018_.NET_Homework5.Infrastructure.Abstractions;

namespace Academy2018_.NET_Homework5.Infrastructure.Models
{
    public class AirplaneType : IEntity
    {
        public int Id { get; set; }

        public string AirplaneModel { get; set; }

        public int SeatsCount { get; set; }

        public int CarryingCapacity { get; set; }
    }
}
