using Academy2018_.NET_Homework5.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Academy2018_.NET_Homework5.Infrastructure.Database.Configurations
{
    public class AirplaneTypesConfiguration: IEntityTypeConfiguration<AirplaneType>
    {
        public void Configure(EntityTypeBuilder<AirplaneType> builder)
        {
            builder.HasData(
                new AirplaneType
                {
                    Id = 1,
                    AirplaneModel = "Typolev Ty-134",
                    CarryingCapacity = 47000,
                    SeatsCount = 96
                },
                new AirplaneType
                {
                    Id = 2,
                    AirplaneModel = "Typolev Ty-154",
                    CarryingCapacity = 52000,
                    SeatsCount = 158
                },
                new AirplaneType
                {
                    Id = 3,
                    AirplaneModel = "Sukhoi SuperJet-100",
                    CarryingCapacity = 45900,
                    SeatsCount = 86
                },
                new AirplaneType
                {
                    Id = 4,
                    AirplaneModel = "Illyushin IL-62",
                    CarryingCapacity = 280300,
                    SeatsCount = 198
                },
                new AirplaneType
                {
                    Id = 5,
                    AirplaneModel = "Airbus A310",
                    CarryingCapacity = 164000,
                    SeatsCount = 183
                });
        }
    }
}
