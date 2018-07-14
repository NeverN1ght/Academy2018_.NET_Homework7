using System;
using Academy2018_.NET_Homework5.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Academy2018_.NET_Homework5.Infrastructure.Database.Configurations
{
    public class PilotsConfiguration : IEntityTypeConfiguration<Pilot>
    {
        public void Configure(EntityTypeBuilder<Pilot> builder)
        {
            builder.HasData(
                new Pilot {
                    Id = 1,
                    FirstName = "Petro",
                    LastName = "Chernov",
                    Birthdate = new DateTime(1990, 2, 5),
                    Experience = 5
                },
                new Pilot {
                    Id = 2,
                    FirstName = "Alex",
                    LastName = "Yaschenko",
                    Birthdate = new DateTime(1983, 6, 28),
                    Experience = 3
                },
                new Pilot {
                    Id = 3,
                    FirstName = "Ivan",
                    LastName = "Golub",
                    Birthdate = new DateTime(1993, 12, 31),
                    Experience = 1
                },
                new Pilot {
                    Id = 4,
                    FirstName = "Anrew",
                    LastName = "Novikov",
                    Birthdate = new DateTime(1975, 8, 14),
                    Experience = 8
                });
        }
    }
}
