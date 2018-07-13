using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Academy2018_.NET_Homework4.Core.Abstractions;
using Academy2018_.NET_Homework4.Core.Services;
using Academy2018_.NET_Homework4.Shared.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Academy2018_.NET_Homework4.API.Controllers
{
    [Produces("application/json")]
    [Route("api/Pilots")]
    public class PilotsController : Controller
    {
        private readonly IService<PilotDto> _pilotsService;

        public PilotsController(IService<PilotDto> pilotsService)
        {
            _pilotsService = pilotsService;
        }

        // GET: api/Pilots
        [HttpGet]
        public IEnumerable<PilotDto> Get()
        {
            return _pilotsService.GetAll();
        }

        // GET: api/Pilots/5
        [HttpGet("{id}"/*, Name = "Get"*/)]
        public PilotDto Get(int id)
        {
            return _pilotsService.GetById(id);
        }
        
        // POST: api/Pilots
        [HttpPost]
        public void Post([FromBody]PilotDto pilotDto)
        {
            _pilotsService.Add(pilotDto);
        }
        
        // PUT: api/Pilots/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]PilotDto pilotDto)
        {
            _pilotsService.Update(id, pilotDto);
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _pilotsService.Delete(id);
        }
    }
}
