using System.Collections.Generic;
using System.Linq;
using Academy2018_.NET_Homework4.Core.Abstractions;
using Academy2018_.NET_Homework4.Infrastructure.Abstractions;
using Academy2018_.NET_Homework4.Infrastructure.Models;
using Academy2018_.NET_Homework4.Shared.DTOs;
using AutoMapper;

namespace Academy2018_.NET_Homework4.Core.Services
{
    public class AirplanesService: IService<AirplaneDto>
    {
        private readonly IRepository<Airplane> _repository;
        private readonly IMapper _mapper;

        public AirplanesService(IRepository<Airplane> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public IEnumerable<AirplaneDto> GetAll()
        {
            return _mapper.Map<IEnumerable<Airplane>, IEnumerable<AirplaneDto>>(
                _repository.Get());
        }

        public AirplaneDto GetById(object id)
        {
            return _mapper.Map<Airplane, AirplaneDto>(
                _repository.Get().FirstOrDefault(a => a.Id == (int)id));
        }

        public void Add(AirplaneDto dto)
        {
            _repository.Create(
                _mapper.Map<AirplaneDto, Airplane>(dto));
        }

        public void Update(object id, AirplaneDto dto)
        {
            _repository.Update((int)id,
                _mapper.Map<AirplaneDto,Airplane>(dto));
        }

        public void Delete(object id)
        {
            _repository.Delete((int)id);
        }
    }
}
