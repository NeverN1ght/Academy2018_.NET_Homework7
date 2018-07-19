using System;
using System.Threading.Tasks;
using Academy2018_.NET_Homework5.Core.Abstractions;
using Academy2018_.NET_Homework5.Core.Services.Data;
using Academy2018_.NET_Homework5.Shared.DTOs;
using Academy2018_.NET_Homework5.Shared.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Academy2018_.NET_Homework5.API.Controllers
{
    [Produces("application/json")]
    [Route("api/Crews")]
    public class CrewsController : Controller
    {
        private readonly IService<CrewDto> _crewsService;
        private readonly CrewsLoadService _loadService;

        public CrewsController(IService<CrewDto> crewsService, CrewsLoadService loadService)
        {
            _crewsService = crewsService;
            _loadService = loadService;
        }

        // GET: api/Crews
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _crewsService.GetAllAsync());
        }

        // GET: api/Crews/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                return Ok(await _crewsService.GetByIdAsync(id));
            }
            catch (NotExistException)
            {
                return NotFound();
            }
        }

        // GET: api/Crews/Load
        [HttpGet("Load")]
        public async Task<IActionResult> Load()
        {
            try
            {
                await _loadService.LoadLogAndSaveDataAsync("http://5b128555d50a5c0014ef1204.mockapi.io/crew");
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/Crews
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CrewDto dto)
        {
            try
            {
                var createdId = await _crewsService.AddAsync(dto);
                return CreatedAtAction("Get",
                    await _crewsService.GetByIdAsync(createdId));
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
        
        // PUT: api/Crews/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]CrewDto dto)
        {
            try
            {
                await _crewsService.UpdateAsync(id, dto);
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
                await _crewsService.DeleteAsync(id);
                return NoContent();
            }
            catch (NotExistException)
            {
                return NotFound();
            }
        }
    }
}
