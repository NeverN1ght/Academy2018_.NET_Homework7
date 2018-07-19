using System.Threading.Tasks;
using Academy2018_.NET_Homework5.Core.Abstractions;
using Academy2018_.NET_Homework5.Shared.DTOs;
using Academy2018_.NET_Homework5.Shared.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Academy2018_.NET_Homework5.API.Controllers
{
    [Produces("application/json")]
    [Route("api/Departures")]
    public class DeparturesController : Controller
    {
        private readonly IService<DepartureDto> _departureService;

        public DeparturesController(IService<DepartureDto> departureService)
        {
            _departureService = departureService;
        }

        // GET: api/Departures
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _departureService.GetAllAsync());
        }

        // GET: api/Departures/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                return Ok(await _departureService.GetByIdAsync(id));
            }
            catch (NotExistException)
            {
                return NotFound();
            }          
        }
        
        // POST: api/Departures
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]DepartureDto dto)
        {
            try
            {
                var createdId = await _departureService.AddAsync(dto);
                return CreatedAtAction("Get",
                    await _departureService.GetByIdAsync(createdId));
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
        
        // PUT: api/Departures/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]DepartureDto dto)
        {
            try
            {
                await _departureService.UpdateAsync(id, dto);
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

        // DELETE: api/Departures/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _departureService.DeleteAsync(id);
                return NoContent();
            }
            catch (NotExistException)
            {
                return NotFound();
            }
        }
    }
}
