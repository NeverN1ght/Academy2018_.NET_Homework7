using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Academy2018_.NET_Homework5.API.Middleware;
using Academy2018_.NET_Homework5.Core.Abstractions;
using Academy2018_.NET_Homework5.Core.Services;
using Academy2018_.NET_Homework5.Core.Services.Data;
using Academy2018_.NET_Homework5.Core.Validation;
using Academy2018_.NET_Homework5.Infrastructure.Abstractions;
using Academy2018_.NET_Homework5.Infrastructure.Database;
using Academy2018_.NET_Homework5.Infrastructure.Database.Extensions;
using Academy2018_.NET_Homework5.Infrastructure.Models;
using Academy2018_.NET_Homework5.Infrastructure.Repositories;
using Academy2018_.NET_Homework5.Shared.DTOs;
using Academy2018_.NET_Homework5.Shared.DTOs.Json;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Academy2018_.NET_Homework5.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();


            services.AddCors(options => {
                options.AddPolicy("AllowAllOrigin", 
                    builder => builder
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials()
                    );
            });

            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add(new CorsAuthorizationFilterFactory("AllowAllOrigin"));
            });

            services.AddDbContext<AirportContext>();

            services.AddScoped<IService<PilotDto>, PilotsService>();
            services.AddScoped<IService<FlightDto>, FlightsService>();
            services.AddScoped<IService<DepartureDto>, DeparturesService>();
            services.AddScoped<IService<StewardesseDto>, StewardessesService>();
            services.AddScoped<IService<TicketDto>, TicketsService>();
            services.AddScoped<IService<AirplaneDto>, AirplanesService>();
            services.AddScoped<IService<AirplaneTypeDto>, AirplaneTypesService>();
            services.AddScoped<IService<CrewDto>, CrewsService>();
            services.AddScoped<CrewsLoadService>();
            services.AddScoped<FlightsService>();

            services.AddTransient<AbstractValidator<Pilot>, PilotValidator>();
            services.AddTransient<AbstractValidator<Airplane>, AirplaneValidator>();
            services.AddTransient<AbstractValidator<Departure>, DepartureValidator>();
            services.AddTransient<AbstractValidator<Stewardesse>, StewardesseValidator>();
            services.AddTransient<AbstractValidator<Crew>, CrewValidator>();
            services.AddTransient<AbstractValidator<Flight>, FlightValidator>();
            services.AddTransient<AbstractValidator<Ticket>, TicketValidator>();
            services.AddTransient<AbstractValidator<AirplaneType>, AirplaneTypeValidator>();

            services.AddTransient<IRepository<Pilot>, PilotsRepository>();
            services.AddTransient<IRepository<Flight>, FlightsRepository>();
            services.AddTransient<IRepository<Departure>, DeparturesRepository>();
            services.AddTransient<IRepository<Stewardesse>, StewardessesRepository>();
            services.AddTransient<IRepository<Ticket>, TicketsRepository>();
            services.AddTransient<IRepository<Airplane>, AirplanesRepository>();
            services.AddTransient<IRepository<AirplaneType>, AirplaneTypesRepository>();
            services.AddTransient<IRepository<Crew>, CrewsRepository>();
            services.AddTransient<CrewsRepository>();
            services.AddTransient<FlightsRepository>();
            
            var mapper = MapperConfiguration().CreateMapper();
            services.AddTransient(_ => mapper);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // preparing database
            using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = scope.ServiceProvider.GetService<AirportContext>();
                context.EnsureDatabaseSeeded();
            }

            app.UseMvc();
            app.UseCorsMiddleware();
            app.UseCors("AllowAllOrigin");

            app.UseCors(builder => builder
                .WithOrigins("http://localhost:4200")
                .AllowAnyOrigin()
                .AllowCredentials()
                .AllowAnyHeader()
                .AllowAnyMethod()
            );
        }

        public MapperConfiguration MapperConfiguration()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Pilot, PilotDto>();
                cfg.CreateMap<PilotDto, Pilot>();

                cfg.CreateMap<Flight, FlightDto>();
                cfg.CreateMap<FlightDto, Flight>();

                cfg.CreateMap<Stewardesse, StewardesseDto>();
                cfg.CreateMap<StewardesseDto, Stewardesse>();

                cfg.CreateMap<Ticket, TicketDto>();
                cfg.CreateMap<TicketDto, Ticket>();

                cfg.CreateMap<Crew, CrewDto>();
                cfg.CreateMap<CrewDto, Crew>();

                cfg.CreateMap<Airplane, AirplaneDto>();
                cfg.CreateMap<AirplaneDto, Airplane>();

                cfg.CreateMap<AirplaneType, AirplaneTypeDto>();
                cfg.CreateMap<AirplaneTypeDto, AirplaneType>();

                cfg.CreateMap<Departure, DepartureDto>();
                cfg.CreateMap<DepartureDto, Departure>();

                cfg.CreateMap<JsonPilotDto, Pilot>()
                    .ForMember(p => p.FirstName, opt => opt.MapFrom(j => j.FirstName))
                    .ForMember(p => p.LastName, opt => opt.MapFrom(j => j.LastName))
                    .ForMember(p => p.Birthdate, opt => opt.MapFrom(j => j.BirthDate))
                    .ForMember(p => p.Experience, opt => opt.MapFrom(j => j.Exp))
                    .ForMember(p => p.Id, opt => opt.UseValue(0));

                cfg.CreateMap<JsonStewardessDto, Stewardesse>()
                    .ForMember(s => s.FirstName, opt => opt.MapFrom(j => j.FirstName))
                    .ForMember(s => s.LastName, opt => opt.MapFrom(j => j.LastName))
                    .ForMember(s => s.Birthdate, opt => opt.MapFrom(j => j.BirthDate))
                    .ForMember(s => s.Id, opt => opt.UseValue(0));

                cfg.CreateMap<JsonCrewDto, Crew>()
                    .ForMember(c => c.Pilot, opt => opt.MapFrom(j => j.Pilot.First()))
                    .ForMember(c => c.Stewardesses, opt => opt.MapFrom(j => j.Stewardess))
                    .ForMember(c => c.Id, opt => opt.UseValue(0));
            });

            return config;
        }
    }
}
