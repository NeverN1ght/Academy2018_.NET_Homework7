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
    [Route("api/Tickets")]
    public class TicketsController : Controller
    {
        private readonly IService<TicketDto> _ticketsService;

        public TicketsController(IService<TicketDto> ticketsService)
        {
            _ticketsService = ticketsService;
        }

        // GET: api/Tickets
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _ticketsService.GetAllAsync());
        }

        // GET: api/Tickets/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                return Ok(await _ticketsService.GetByIdAsync(id));
            }
            catch (NotExistException)
            {
                return NotFound();
            }
        }
        
        // POST: api/Tickets
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]TicketDto dto)
        {
            try
            {
                var createdId = await _ticketsService.AddAsync(dto);
                return CreatedAtAction("Get",
                    await _ticketsService.GetByIdAsync(createdId));
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
        
        // PUT: api/Tickets/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]TicketDto dto)
        {
            try
            {
                await _ticketsService.UpdateAsync(id, dto);
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
                await _ticketsService.DeleteAsync(id);
                return NoContent();
            }
            catch (NotExistException)
            {
                return NotFound();
            }
        }
    }
}
