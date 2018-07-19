using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using Academy2018_.NET_Homework5.Infrastructure.Abstractions;
using Academy2018_.NET_Homework5.Infrastructure.Database;
using Academy2018_.NET_Homework5.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Academy2018_.NET_Homework5.Infrastructure.Repositories
{
    public class FlightsRepository: IRepository<Flight>
    {
        private readonly AirportContext _ctx;

        public FlightsRepository(AirportContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<List<Flight>> GetAsync()
        {
            return await _ctx.Flights
                .Include(f => f.Tickets)
                .ToListAsync();
        }

        public Task<Flight> GetClosestInTimeWithDelay()
        {
            var taskCompletionSource = new TaskCompletionSource<Flight>();
            var timer = new Timer
            {
                Interval = 5000,
                Enabled = true
            };
            timer.Elapsed += (source, args) => {
                try
                {
                    taskCompletionSource.SetResult(
                        _ctx.Flights
                            .Include(f => f.Tickets)
                            .OrderBy(f => f.ArrivalTime)
                            .First()
                        );

                    timer.Enabled = false;
                }
                catch (Exception ex)
                {
                    taskCompletionSource.SetException(ex);
                }
            };
            return taskCompletionSource.Task;
        }

        public async Task<Flight> GetAsync(object id)
        {
            return await _ctx.Flights.FindAsync(id);
        }

        public async Task<object> CreateAsync(Flight entity)
        {
            await _ctx.Flights.AddAsync(entity);

            // make save to get created entity id
            await _ctx.SaveChangesAsync();
            return entity.Number;
        }

        public async Task UpdateAsync(object id, Flight entity)
        {
            entity.Number = (string)id;
            var existedEntity = await _ctx.Flights.FindAsync(id);
            _ctx.Entry(existedEntity).CurrentValues.SetValues(entity);
        }

        public async Task DeleteAsync(object id)
        {
            var entity = await _ctx.Flights.FindAsync(id);
            await DeleteAsync(entity);
        }

        public async Task DeleteAsync(Flight entity)
        {
            await Task.Run(() => _ctx.Flights.Remove(entity));
        }

        public async Task SaveChangesAsync()
        {
            await _ctx.SaveChangesAsync();
        }

        public async Task<bool> IsExistAsync(object id)
        {
            return await _ctx.Flights.FindAsync(id) != null;
        }
    }
}
