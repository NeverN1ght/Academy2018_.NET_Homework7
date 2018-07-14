using System;
using System.Collections.Generic;
using Academy2018_.NET_Homework5.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Academy2018_.NET_Homework5.Infrastructure.Database.Configurations
{
    public class CrewsConfiguration : IEntityTypeConfiguration<Crew>
    {
        public void Configure(EntityTypeBuilder<Crew> builder)
        {
            builder.HasData(
                new Crew
                {
                    Id = 1,
                    Pilot = new Pilot
                    {
                        Id = 12,
                        Birthdate = new DateTime(1987, 3 , 1),
                        Experience = 6,
                        FirstName = "Sasha",
                        LastName = "Saharov"
                    },
                    Stewardesses = new List<Stewardesse> {
                        new Stewardesse
                        {
                            Id = 21,
                            Birthdate = new DateTime(1989, 1, 2),
                            FirstName = "Samira",
                            LastName = "Elbrus"
                        },
                        new Stewardesse
                        {
                            Id = 22,
                            Birthdate = new DateTime(1989, 1, 3),
                            FirstName = "Elvira",
                            LastName = "Elbrus"
                        }
                    }
                },
                new Crew
                {
                    Id = 2,
                    Pilot = new Pilot
                    {
                        Id = 32,
                        Birthdate = new DateTime(1991, 12, 2),
                        FirstName = "Maxim",
                        LastName = "Suvorov",
                        Experience = 15
                    },
                    Stewardesses = new List<Stewardesse> {
                        new Stewardesse
                        {
                            Id = 18,
                            Birthdate = new DateTime(1989, 1, 5),
                            FirstName = "Valeriya",
                            LastName = "Solomko"
                        }
                    }
                },
                new Crew
                {
                    Id = 3,
                    Pilot = new Pilot
                    {
                        Id = 41,
                        Birthdate = new DateTime(1991, 10, 2),
                        FirstName = "Vadim",
                        LastName = "Melnik",
                        Experience = 15
                    },
                    Stewardesses = new List<Stewardesse> {
                        new Stewardesse
                        {
                            Id = 19,
                            Birthdate = new DateTime(1979, 2, 5),
                            FirstName = "Irina",
                            LastName = "Natamina"
                        }
                    }
                });
        }
    }
}
