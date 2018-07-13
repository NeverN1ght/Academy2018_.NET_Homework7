using System;
using System.Collections.Generic;
using Academy2018_.NET_Homework4.Infrastructure.Abstractions;
using Academy2018_.NET_Homework4.Infrastructure.Data;
using Academy2018_.NET_Homework4.Infrastructure.Models;

namespace Academy2018_.NET_Homework4.Infrastructure.Repositories
{
    public class FlightsRepository: IRepository<Flight>
    {
        private readonly DataSource _dataSource;

        public FlightsRepository(DataSource dataSource)
        {
            _dataSource = dataSource;
        }

        public IEnumerable<Flight> Get()
        {
            return _dataSource.Flights;
        }

        public void Create(Flight entity)
        {
            entity.Number = Guid.NewGuid();
            _dataSource.Flights.Add(entity);
        }

        public void Update(object id, Flight entity)
        {
            Delete(id);
            entity.Number = (Guid)id;
            _dataSource.Flights.Add(entity);
        }

        public void Delete(object id)
        {
            var entity = _dataSource.Flights.Find(f => f.Number == (Guid)id);
            Delete(entity);
        }

        public void Delete(Flight entity)
        {
            _dataSource.Flights.Remove(entity);
        }
    }
}
