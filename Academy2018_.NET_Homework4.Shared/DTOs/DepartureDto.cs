using System;
using Academy2018_.NET_Homework4.Infrastructure.Models;

namespace Academy2018_.NET_Homework4.Shared.DTOs
{
    public class DepartureDto
    {
        public int Id { get; set; }

        public Flight Flight { get; set; }

        public DateTime DepartureTime { get; set; }

        public Crew Crew { get; set; }

        public Airplane Airplane { get; set; }
    }
}
