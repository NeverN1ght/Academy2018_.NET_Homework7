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
    [Route("api/Stewardesses")]
    public class StewardessesController : Controller
    {
        private readonly IService<StewardesseDto> _stewardessesService;

        public StewardessesController(IService<StewardesseDto> stewardessesService)
        {
            _stewardessesService = stewardessesService;
        }

        // GET: api/Stewardesses
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _stewardessesService.GetAllAsync());
        }

        // GET: api/Stewardesses/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                return Ok(await _stewardessesService.GetByIdAsync(id));
            }
            catch (NotExistException)
            {
                return NotFound();
            }
        }
        
        // POST: api/Stewardesses
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]StewardesseDto dto)
        {
            try
            {
                var createdId = await _stewardessesService.AddAsync(dto);
                return CreatedAtAction("Get",
                    await _stewardessesService.GetByIdAsync(createdId));
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
        
        // PUT: api/Stewardesses/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]StewardesseDto dto)
        {
            try
            {
                await _stewardessesService.UpdateAsync(id, dto);
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
                await _stewardessesService.DeleteAsync(id);
                return NoContent();
            }
            catch (NotExistException)
            {
                return NotFound();
            }
        }
    }
}
