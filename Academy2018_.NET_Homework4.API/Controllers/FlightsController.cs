using System;
using System.Threading.Tasks;
using Academy2018_.NET_Homework5.Core.Abstractions;
using Academy2018_.NET_Homework5.Core.Services;
using Academy2018_.NET_Homework5.Shared.DTOs;
using Academy2018_.NET_Homework5.Shared.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Academy2018_.NET_Homework5.API.Controllers
{
    [Produces("application/json")]
    [Route("api/Flights")]
    public class FlightsController : Controller
    {
        private readonly FlightsService _flightsService;

        public FlightsController(FlightsService flightsService)
        {
            _flightsService = flightsService;
        }

        // GET: api/Flights
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _flightsService.GetAllAsync());
        }

        // GET: api/Flights/5
        [HttpGet("{number}")]
        public async Task<IActionResult> Get(string number)
        {
            try
            {
                return Ok(await _flightsService.GetByIdAsync(number));
            }
            catch (NotExistException)
            {
                return NotFound();
            }
        }

        // GET: api/Flights/Delay
        [HttpGet("Delay")]
        public async Task<IActionResult> QueryWithDelay()
        {
            try
            {
                return Ok(await _flightsService.GetFlightQueryWithDelay());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/Flights
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]FlightDto dto)
        {
            try
            {
                var createdId = await _flightsService.AddAsync(dto);
                return CreatedAtAction("Get",
                    await _flightsService.GetByIdAsync(createdId));
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Errors);
            }
            catch (NullBodyException)
            {
                return BadRequest();
            }
            catch (DbUpdateException ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }
        
        // PUT: api/Flights/5
        [HttpPut("{number}")]
        public async Task<IActionResult> Put(string number, [FromBody] FlightDto dto)
        {
            try
            {
                await _flightsService.UpdateAsync(number, dto);
                return Ok();
            }
            catch (NotExistException)
            {
                return NotFound();
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Errors);
            }
            catch (NullBodyException)
            {
                return BadRequest();
            }
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{number}")]
        public async Task<IActionResult> Delete(string number)
        {
            try
            {
                await _flightsService.DeleteAsync(number);
                return NoContent();
            }
            catch (NotExistException)
            {
                return NotFound();
            }
        }
    }
}
