using System;
using Academy2018_.NET_Homework5.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Academy2018_.NET_Homework5.Infrastructure.Database.Configurations
{
    public class AirplanesConfiguration: IEntityTypeConfiguration<Airplane>
    {
        public void Configure(EntityTypeBuilder<Airplane> builder)
        {

            builder.HasOne<AirplaneType>(t => t.Type);
            builder.HasData(
                new Airplane
                {
                    Id = 1,
                    Name = "A. Dayneka",
                    Type = new AirplaneType
                    {
                        Id = 12,
                        AirplaneModel = "NYA-226",
                        CarryingCapacity = 15000,
                        SeatsCount = 987
                    },
                    ExploitationTerm = new TimeSpan(1000, 0, 0, 0),
                    ReleaseDate = new DateTime(1985, 5, 21)
                },
                new Airplane
                {
                    Id = 2,
                    Name = "Manchester United",
                    Type = new AirplaneType
                    {
                        Id = 13,
                        AirplaneModel = "MNA-566",
                        CarryingCapacity = 19000,
                        SeatsCount = 876
                    },
                    ExploitationTerm = new TimeSpan(1200, 0, 0, 0),
                    ReleaseDate = new DateTime(1990, 7, 12)
                },
                new Airplane
                {
                    Id = 3,
                    Name = "Retro",
                    Type = new AirplaneType
                    {
                        Id = 14,
                        AirplaneModel = "OKA-221",
                        CarryingCapacity = 30000,
                        SeatsCount = 658
                    },
                    ExploitationTerm = new TimeSpan(900, 0, 0, 0),
                    ReleaseDate = new DateTime(1980, 12, 11)
                });
        }
    }
}
