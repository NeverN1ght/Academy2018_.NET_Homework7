using System.Collections.Generic;
using System.Linq;
using Academy2018_.NET_Homework4.Core.Abstractions;
using Academy2018_.NET_Homework4.Infrastructure.Abstractions;
using Academy2018_.NET_Homework4.Infrastructure.Models;
using Academy2018_.NET_Homework4.Shared.DTOs;
using AutoMapper;

namespace Academy2018_.NET_Homework4.Core.Services
{
    public class DeparturesService: IService<DepartureDto>
    {
        private readonly IRepository<Departure> _repository;
        private readonly IMapper _mapper;

        public DeparturesService(IRepository<Departure> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public IEnumerable<DepartureDto> GetAll()
        {
            return _mapper.Map<IEnumerable<Departure>, IEnumerable<DepartureDto>>(
                _repository.Get());
        }

        public DepartureDto GetById(object id)
        {
            return _mapper.Map<Departure, DepartureDto>(
                _repository.Get().FirstOrDefault(d => d.Id == (int)id));
        }

        public void Add(DepartureDto dto)
        {
            _repository.Create(
                _mapper.Map<DepartureDto, Departure>(dto));
        }

        public void Update(object id, DepartureDto dto)
        {
            _repository.Update((int)id,
                _mapper.Map<DepartureDto, Departure>(dto));
        }

        public void Delete(object id)
        {
            _repository.Delete((int)id);
        }
    }
}
