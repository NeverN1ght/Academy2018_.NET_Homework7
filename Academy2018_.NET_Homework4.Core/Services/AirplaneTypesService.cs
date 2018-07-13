using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Academy2018_.NET_Homework4.Core.Abstractions;
using Academy2018_.NET_Homework4.Infrastructure.Abstractions;
using Academy2018_.NET_Homework4.Infrastructure.Models;
using Academy2018_.NET_Homework4.Shared.DTOs;
using AutoMapper;

namespace Academy2018_.NET_Homework4.Core.Services
{
    public class AirplaneTypesService: IService<AirplaneTypeDto>
    {
        private readonly IRepository<AirplaneType> _repository;
        private readonly IMapper _mapper;

        public AirplaneTypesService(IRepository<AirplaneType> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public IEnumerable<AirplaneTypeDto> GetAll()
        {
            return _mapper.Map<IEnumerable<AirplaneType>, IEnumerable<AirplaneTypeDto>>(
                _repository.Get());
        }

        public AirplaneTypeDto GetById(object id)
        {
            return _mapper.Map<AirplaneType, AirplaneTypeDto>(
                _repository.Get().FirstOrDefault(a => a.Id == (int)id));
        }

        public void Add(AirplaneTypeDto dto)
        {
            _repository.Create(
                _mapper.Map<AirplaneTypeDto, AirplaneType>(dto));
        }

        public void Update(object id, AirplaneTypeDto dto)
        {
            _repository.Update((int)id,
                _mapper.Map<AirplaneTypeDto, AirplaneType>(dto));
        }

        public void Delete(object id)
        {
            _repository.Delete((int)id);
        }
    }
}
