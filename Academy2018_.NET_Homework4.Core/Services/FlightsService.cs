using System;
using System.Collections.Generic;
using System.Linq;
using Academy2018_.NET_Homework4.Core.Abstractions;
using Academy2018_.NET_Homework4.Infrastructure.Abstractions;
using Academy2018_.NET_Homework4.Infrastructure.Models;
using Academy2018_.NET_Homework4.Shared.DTOs;
using AutoMapper;

namespace Academy2018_.NET_Homework4.Core.Services
{
    public class FlightsService: IService<FlightDto>
    {
        private readonly IRepository<Flight> _repository;
        private readonly IMapper _mapper;

        public FlightsService(IRepository<Flight> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public IEnumerable<FlightDto> GetAll()
        {
            return _mapper.Map<IEnumerable<Flight>, IEnumerable<FlightDto>>(
                _repository.Get());
        }

        public FlightDto GetById(object id)
        {
            return _mapper.Map<Flight, FlightDto>(
                _repository.Get().FirstOrDefault(f => f.Number == (Guid)id));
        }

        public void Add(FlightDto dto)
        {
            _repository.Create(
                _mapper.Map<FlightDto, Flight>(dto));
        }

        public void Update(object id, FlightDto dto)
        {
            _repository.Update((Guid)id,
                _mapper.Map<FlightDto, Flight>(dto));
        }

        public void Delete(object id)
        {
            _repository.Delete((Guid)id);
        }
    }
}
