using Academy2018_.NET_Homework5.Infrastructure.Database.Configurations;
using Academy2018_.NET_Homework5.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Academy2018_.NET_Homework5.Infrastructure.Database
{
    public class AirportContext: DbContext
    {
        public AirportContext(DbContextOptions<AirportContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Airplane> Airplanes { get; set; }
        public DbSet<AirplaneType> AirplaneTypes { get; set; }
        public DbSet<Crew> Crews { get; set; }
        public DbSet<Departure> Departures { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Pilot> Pilots { get; set; }
        public DbSet<Stewardesse> Stewardesses { get; set; }
        public DbSet<Ticket> Tickets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AirplanesConfiguration());
            modelBuilder.ApplyConfiguration(new PilotsConfiguration());
            modelBuilder.ApplyConfiguration(new AirplaneTypesConfiguration());
            modelBuilder.ApplyConfiguration(new TicketsConfiguration());
            modelBuilder.ApplyConfiguration(new StewardessesConfiguration());
            modelBuilder.ApplyConfiguration(new CrewsConfiguration());
            modelBuilder.ApplyConfiguration(new DeparturesConfiguration());
            modelBuilder.ApplyConfiguration(new FlightsConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
