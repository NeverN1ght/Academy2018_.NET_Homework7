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
    [Route("api/Airplanes")]
    public class AirplanesController : Controller
    {
        private readonly IService<AirplaneDto> _airplanesService;

        public AirplanesController(IService<AirplaneDto> airplanesService)
        {
            _airplanesService = airplanesService;
        }

        // GET: api/Airplanes
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _airplanesService.GetAllAsync());
        }

        // GET: api/Airplanes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                return Ok(await _airplanesService.GetByIdAsync(id));
            }
            catch (NotExistException)
            {
                return NotFound();
            }
        }
        
        // POST: api/Airplanes
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]AirplaneDto dto)
        {
            try
            {
                var createdId = await _airplanesService.AddAsync(dto);
                return CreatedAtAction("Get",
                    await _airplanesService.GetByIdAsync(createdId));
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
        
        // PUT: api/Airplanes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]AirplaneDto dto)
        {
            try
            {
                await _airplanesService.UpdateAsync(id, dto);
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
                await _airplanesService.DeleteAsync(id);
                return NoContent();
            }
            catch (NotExistException)
            {
                return NotFound();
            }
        }
    }
}
