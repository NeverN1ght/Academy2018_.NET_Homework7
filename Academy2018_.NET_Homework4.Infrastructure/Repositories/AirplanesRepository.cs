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
    public class AirplanesRepository: BasicRepository<Airplane>
    {
        private readonly AirportContext _ctx;

        public AirplanesRepository(AirportContext ctx): base(ctx)
        {
            _ctx = ctx;
        }

        public override async Task<List<Airplane>> GetAsync()
        {
            return await _ctx.Airplanes
                .Include(a => a.Type)
                .ToListAsync();
        }
    }
}
