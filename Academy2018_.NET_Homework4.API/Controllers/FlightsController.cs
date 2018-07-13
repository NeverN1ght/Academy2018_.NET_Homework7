using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Academy2018_.NET_Homework4.Core.Abstractions;
using Academy2018_.NET_Homework4.Shared.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Academy2018_.NET_Homework4.API.Controllers
{
    [Produces("application/json")]
    [Route("api/Flights")]
    public class FlightsController : Controller
    {
        private readonly IService<FlightDto> _flightsService;

        public FlightsController(IService<FlightDto> flightsService)
        {
            _flightsService = flightsService;
        }

        // GET: api/Flights
        [HttpGet]
        public IEnumerable<FlightDto> Get()
        {
            return _flightsService.GetAll();
        }

        // GET: api/Flights/5
        [HttpGet("{id}"/*, Name = "Get"*/)]
        public FlightDto Get(Guid number)
        {
            return _flightsService.GetById(number);
        }
        
        // POST: api/Flights
        [HttpPost]
        public void Post([FromBody]FlightDto dto)
        {
            _flightsService.Add(dto);
        }
        
        // PUT: api/Flights/5
        [HttpPut("{id}")]
        public void Put(Guid number, [FromBody]FlightDto dto)
        {
            _flightsService.Update(number, dto);
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(Guid number)
        {
            _flightsService.Delete(number);
        }
    }
}
