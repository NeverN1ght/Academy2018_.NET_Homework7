using System;
using System.Collections.Generic;
using Academy2018_.NET_Homework5.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Academy2018_.NET_Homework5.Infrastructure.Database.Configurations
{
    public class FlightsConfiguration: IEntityTypeConfiguration<Flight>
    {
        public void Configure(EntityTypeBuilder<Flight> builder)
        {
            builder.HasData(
                new Flight
                {
                    Number = "DY-2891",
                    ArrivalTime = new DateTime(2018, 07, 12, 12, 24, 0),
                    DeparturePoint = "Borispol, Ukraine",
                    DestinationPoint = "New York, USA",
                    Tickets = new List<Ticket>
                    {
                        new Ticket
                        {
                            Id = 25,
                            FlightNumber = "DY-2891",
                            Price = 300
                        },
                        new Ticket
                        {
                            Id = 26,
                            FlightNumber = "DY-2891",
                            Price = 600
                        }
                    }
                },
                new Flight
                {
                    Number = "AC-3948",
                    ArrivalTime = new DateTime(2018, 08, 15, 14, 12, 0),
                    DeparturePoint = "Borispol, Ukraine",
                    DestinationPoint = "Moscow, Russia",
                    Tickets = new List<Ticket>
                    {
                        new Ticket
                        {
                            Id = 27,
                            FlightNumber = "AC-3948",
                            Price = 400
                        },
                        new Ticket
                        {
                            Id = 28,
                            FlightNumber = "AC-3948",
                            Price = 700
                        }
                    }
                },
                new Flight
                {
                    Number = "KO-8712",
                    ArrivalTime = new DateTime(2018, 10, 5, 20, 21, 0),
                    DeparturePoint = "Borispol, Ukraine",
                    DestinationPoint = "Katowice, Poland",
                    Tickets = new List<Ticket>
                    {
                        new Ticket
                        {
                            Id = 29,
                            FlightNumber = "AC-3948",
                            Price = 1300
                        },
                        new Ticket
                        {
                            Id = 30,
                            FlightNumber = "AC-3948",
                            Price = 6000
                        }
                    }
                });
        }
    }
}
