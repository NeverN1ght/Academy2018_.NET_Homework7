using System;
using System.ComponentModel.DataAnnotations;
using Academy2018_.NET_Homework5.Infrastructure.Abstractions;

namespace Academy2018_.NET_Homework5.Infrastructure.Models
{
    public class Airplane: IEntity
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public AirplaneType Type { get; set; }

        public DateTime ReleaseDate { get; set; }

        public TimeSpan ExploitationTerm { get; set; }
    }
}
