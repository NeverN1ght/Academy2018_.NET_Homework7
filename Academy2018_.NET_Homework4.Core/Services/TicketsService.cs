using System.Collections.Generic;
using System.Linq;
using Academy2018_.NET_Homework4.Core.Abstractions;
using Academy2018_.NET_Homework4.Infrastructure.Abstractions;
using Academy2018_.NET_Homework4.Infrastructure.Models;
using Academy2018_.NET_Homework4.Shared.DTOs;
using AutoMapper;

namespace Academy2018_.NET_Homework4.Core.Services
{
    public class TicketsService: IService<TicketDto>
    {
        private readonly IRepository<Ticket> _repository;
        private readonly IMapper _mapper;

        public TicketsService(IRepository<Ticket> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public IEnumerable<TicketDto> GetAll()
        {
            return _mapper.Map<IEnumerable<Ticket>, IEnumerable<TicketDto>>(
                _repository.Get());
        }

        public TicketDto GetById(object id)
        {
            return _mapper.Map<Ticket, TicketDto>(
                _repository.Get().FirstOrDefault(t => t.Id == (int)id));
        }

        public void Add(TicketDto dto)
        {
            _repository.Create(
                _mapper.Map<TicketDto, Ticket>(dto));
        }

        public void Update(object id, TicketDto dto)
        {
            _repository.Update((int)id,
                _mapper.Map<TicketDto, Ticket>(dto));
        }

        public void Delete(object id)
        {
            _repository.Delete((int)id);
        }
    }
}
