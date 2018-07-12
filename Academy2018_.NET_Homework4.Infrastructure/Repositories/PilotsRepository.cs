using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Academy2018_.NET_Homework4.Infrastructure.Abstractions;
using Academy2018_.NET_Homework4.Infrastructure.Data;
using Academy2018_.NET_Homework4.Infrastructure.Models;

namespace Academy2018_.NET_Homework4.Infrastructure.Repositories
{
    class PilotsRepository: IRepository<Pilot>
    {
        private readonly DataSource _dataSource;

        public PilotsRepository(DataSource dataSource)
        {
            _dataSource = dataSource;
        }

        public IEnumerable<Pilot> Get(Func<Pilot, bool> filter = null)
        {
            if (filter != null)
            {
                return _dataSource.Pilots.Where(filter);
            }

            return _dataSource.Pilots;
        }

        public void Create(Pilot pilot, string createdBy = null)
        {
            _dataSource.Pilots.Add(pilot);
        }

        public void Update(Pilot pilot, string updatedBy = null)
        {
            var existedPilot = _dataSource.Pilots.Find(p => p.Id == pilot.Id);
            if (existedPilot != null)
            {
                existedPilot.Id = pilot.Id;
                existedPilot.FirstName = pilot.FirstName;
                existedPilot.LastName = pilot.LastName;
                existedPilot.Birthdate = pilot.Birthdate;
                existedPilot.Experience = pilot.Experience;
            }
        }

        public void Delete(object id)
        {
            var pilot = _dataSource.Pilots.Find(p => p.Id == (int)id);
            Delete(pilot);
        }

        public void Delete(Pilot pilot)
        {
            _dataSource.Pilots.Remove(pilot);
        }
    }
}
