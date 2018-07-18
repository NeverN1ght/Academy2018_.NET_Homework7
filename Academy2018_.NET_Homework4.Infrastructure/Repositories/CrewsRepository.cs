using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Academy2018_.NET_Homework5.Infrastructure.Abstractions;
using Academy2018_.NET_Homework5.Infrastructure.Database;
using Academy2018_.NET_Homework5.Infrastructure.Models;
using Academy2018_.NET_Homework5.Infrastructure.Repositories.Basic;
using Microsoft.EntityFrameworkCore;

namespace Academy2018_.NET_Homework5.Infrastructure.Repositories
{
    public class CrewsRepository: BasicRepository<Crew>
    {
        private readonly AirportContext _ctx;

        public CrewsRepository(AirportContext ctx): base(ctx)
        {
            _ctx = ctx;
        }

        public override async Task<List<Crew>> GetAsync()
        {
            return await _ctx.Crews
                .Include(c => c.Pilot)
                .Include(c => c.Stewardesses)
                .ToListAsync();
        }

        public async Task AddRangeAsync(List<Crew> crews)
        {
            await _ctx.Crews.AddRangeAsync(crews);
            await _ctx.SaveChangesAsync();
        }
    }
}
