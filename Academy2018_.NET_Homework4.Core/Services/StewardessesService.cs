using System.Collections.Generic;
using System.Linq;
using Academy2018_.NET_Homework4.Core.Abstractions;
using Academy2018_.NET_Homework4.Infrastructure.Abstractions;
using Academy2018_.NET_Homework4.Infrastructure.Models;
using Academy2018_.NET_Homework4.Shared.DTOs;
using AutoMapper;

namespace Academy2018_.NET_Homework4.Core.Services
{
    public class StewardessesService: IService<StewardesseDto>
    {
        private readonly IRepository<Stewardesse> _repository;
        private readonly IMapper _mapper;

        public StewardessesService(IRepository<Stewardesse> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public IEnumerable<StewardesseDto> GetAll()
        {
            return _mapper.Map<IEnumerable<Stewardesse>, IEnumerable<StewardesseDto>>(
                _repository.Get());
        }

        public StewardesseDto GetById(object id)
        {
            return _mapper.Map<Stewardesse, StewardesseDto>(
                _repository.Get().FirstOrDefault(s => s.Id == (int)id));
        }

        public void Add(StewardesseDto dto)
        {
            _repository.Create(
                _mapper.Map<StewardesseDto, Stewardesse>(dto));
        }

        public void Update(object id, StewardesseDto dto)
        {
            _repository.Update((int)id,
                _mapper.Map<StewardesseDto, Stewardesse>(dto));
        }

        public void Delete(object id)
        {
            _repository.Delete((int)id);
        }
    }
}
