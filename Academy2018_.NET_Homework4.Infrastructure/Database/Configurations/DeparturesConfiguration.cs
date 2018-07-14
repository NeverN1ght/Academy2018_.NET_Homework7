using System;
using System.Collections.Generic;
using Academy2018_.NET_Homework5.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Academy2018_.NET_Homework5.Infrastructure.Database.Configurations
{
    public class DeparturesConfiguration: IEntityTypeConfiguration<Departure>
    {
        public void Configure(EntityTypeBuilder<Departure> builder)
        {
            builder.HasData(
                new Departure
                {
                    Id = 1,
                    FlightNumber = "AM-2325",
                    Airplane = new Airplane
                    {
                        Id = 15,
                        ExploitationTerm = TimeSpan.FromDays(1000),
                        Name = "FirstClassPlane",
                        Type = new AirplaneType
                        {
                            Id = 16,
                            SeatsCount = 879,
                            AirplaneModel = "AR-228",
                            CarryingCapacity = 25000
                        }
                    },
                    Crew = new Crew
                    {
                        Id = 17,
                        Stewardesses = new List<Stewardesse>
                        {
                            new Stewardesse
                            {
                                Id = 19,
                                Birthdate = new DateTime(1987, 12, 15),
                                FirstName = "Larisa",
                                LastName = "Bolinskaya"
                            }
                        },
                        Pilot = new Pilot
                        {
                            Id = 20,
                            Birthdate = new DateTime(1978, 5, 21),
                            FirstName = "Bohdan",
                            LastName = "Klavimak",
                            Experience = 21
                        }
                    },
                    DepartureTime = new DateTime(2018, 10, 21, 20, 22, 0)
                });
        }
    }
}
