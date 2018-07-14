using Academy2018_.NET_Homework5.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Academy2018_.NET_Homework5.Infrastructure.Database.Configurations
{
    public class TicketsConfiguration: IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.HasData(
                new Ticket
                {
                    Id = 1,
                    FlightNumber = "DY-2891",
                    Price = 200m
                },
                new Ticket
                {
                    Id = 2,
                    FlightNumber = "AC-3948",
                    Price = 400m
                },
                new Ticket
                {
                    Id = 3,
                    FlightNumber = "DY-2891",
                    Price = 600m
                },
                new Ticket
                {
                    Id = 4,
                    FlightNumber = "KO-8712",
                    Price = 100m
                },
                new Ticket
                {
                    Id = 5,
                    FlightNumber = "AC-3948",
                    Price = 150m
                },
                new Ticket
                {
                    Id = 6,
                    FlightNumber = "KO-8712",
                    Price = 450m
                });
        }
    }
}
