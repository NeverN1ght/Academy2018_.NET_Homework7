using System;
using System.Collections.Generic;

namespace Academy2018_.NET_Homework4.Infrastructure.Models
{
    public class Flight
    {
        public Guid Number { get; set; }

        public string DeparturePoint { get; set; }

        public string DestinationPoint { get; set; }

        public DateTime ArrivalTime { get; set; }

        public List<Ticket> Tickets { get; set; }
    }
}
