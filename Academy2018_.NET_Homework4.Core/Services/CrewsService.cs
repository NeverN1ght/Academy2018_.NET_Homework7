using System.Collections.Generic;
using System.Linq;
using Academy2018_.NET_Homework4.Core.Abstractions;
using Academy2018_.NET_Homework4.Infrastructure.Abstractions;
using Academy2018_.NET_Homework4.Infrastructure.Models;
using Academy2018_.NET_Homework4.Shared.DTOs;
using AutoMapper;

namespace Academy2018_.NET_Homework4.Core.Services
{
    public class CrewsService: IService<CrewDto>
    {
        private readonly IRepository<Crew> _repository;
        private readonly IMapper _mapper;

        public CrewsService(IRepository<Crew> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public IEnumerable<CrewDto> GetAll()
        {
            return _mapper.Map<IEnumerable<Crew>, IEnumerable<CrewDto>>(
                _repository.Get());
        }

        public CrewDto GetById(object id)
        {
            return _mapper.Map<Crew, CrewDto>(
                _repository.Get().FirstOrDefault(c => c.Id == (int)id));
        }

        public void Add(CrewDto dto)
        {
            _repository.Create(
                _mapper.Map<CrewDto, Crew>(dto));
        }

        public void Update(object id, CrewDto dto)
        {
            _repository.Update((int)id,
                _mapper.Map<CrewDto, Crew>(dto));
        }

        public void Delete(object id)
        {
            _repository.Delete((int)id);
        }
    }
}
