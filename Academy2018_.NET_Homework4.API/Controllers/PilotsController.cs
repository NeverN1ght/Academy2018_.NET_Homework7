using System;
using System.Threading.Tasks;
using Academy2018_.NET_Homework5.Core.Abstractions;
using Academy2018_.NET_Homework5.Infrastructure.Database;
using Academy2018_.NET_Homework5.Infrastructure.Models;
using Academy2018_.NET_Homework5.Shared.DTOs;
using Academy2018_.NET_Homework5.Shared.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Academy2018_.NET_Homework5.API.Controllers
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
        public async Task<IActionResult> Get()
        {
            return Ok(await _pilotsService.GetAllAsync());
        }

        // GET: api/Pilots/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                return Ok(await _pilotsService.GetByIdAsync(id));
            }
            catch (NotExistException)
            {
                return NotFound();
            }
        }
        
        // POST: api/Pilots
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]PilotDto pilotDto)
        {
            try
            {
                var createdId = await _pilotsService.AddAsync(pilotDto);
                return CreatedAtAction("Get",
                    await _pilotsService.GetByIdAsync(createdId));
            }
            catch (ValidationException)
            {
                return BadRequest();
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
        
        // PUT: api/Pilots/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]PilotDto pilotDto)
        {
            try
            {
                await _pilotsService.UpdateAsync(id, pilotDto);
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
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _pilotsService.DeleteAsync(id);
                return NoContent();
            }
            catch (NotExistException)
            {
                return NotFound();
            }
        }
    }
}
