using System;
using Academy2018_.NET_Homework5.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Academy2018_.NET_Homework5.Infrastructure.Database.Configurations
{
    public class StewardessesConfiguration: IEntityTypeConfiguration<Stewardesse>
    {
        public void Configure(EntityTypeBuilder<Stewardesse> builder)
        {
            builder.HasData(
                new Stewardesse {
                    Id = 1,
                    FirstName = "Natalia",
                    LastName = "Korobko",
                    Birthdate = new DateTime(1986, 5, 21)
                },
                new Stewardesse {
                    Id = 2,
                    FirstName = "Olha",
                    LastName = "Bilyk",
                    Birthdate = new DateTime(1979, 6, 27)
                },
                new Stewardesse {
                    Id = 3,
                    FirstName = "Nina",
                    LastName = "Ivanova",
                    Birthdate = new DateTime(1995, 9, 17)
                },
                new Stewardesse {
                    Id = 4,
                    FirstName = "Viktoria",
                    LastName = "Petrova",
                    Birthdate = new DateTime(1997, 7, 17)
                },
                new Stewardesse {
                    Id = 5,
                    FirstName = "Daria",
                    LastName = "Kolomiets",
                    Birthdate = new DateTime(1991, 3, 13)
                },
                new Stewardesse {
                    Id = 6,
                    FirstName = "Maria",
                    LastName = "Bondarenko",
                    Birthdate = new DateTime(1994, 10, 5)
                },
                new Stewardesse {
                    Id = 7,
                    FirstName = "Anastasia",
                    LastName = "Golovko",
                    Birthdate = new DateTime(1989, 12, 25)
                });
        }
    }
}
