using Academy2018_.NET_Homework5.Core.Services.Basic;
using Academy2018_.NET_Homework5.Infrastructure.Abstractions;
using Academy2018_.NET_Homework5.Infrastructure.Models;
using Academy2018_.NET_Homework5.Shared.DTOs;
using AutoMapper;
using FluentValidation;

namespace Academy2018_.NET_Homework5.Core.Services
{
    public class FlightsService: BasicService<Flight, FlightDto>
    {
        public FlightsService(
            IRepository<Flight> repository,
            IMapper mapper,
            AbstractValidator<Flight> validator) : base(repository, mapper, validator)
        {
        }
    }
}
