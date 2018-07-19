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
    [Route("api/AirplaneTypes")]
    public class AirplaneTypesController : Controller
    {
        private readonly IService<AirplaneTypeDto> _airplaneTypesService;

        public AirplaneTypesController(IService<AirplaneTypeDto> airplaneTypesService)
        {
            _airplaneTypesService = airplaneTypesService;
        }

        // GET: api/AirplaneTypes
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _airplaneTypesService.GetAllAsync());
        }

        // GET: api/AirplaneTypes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                return Ok(await _airplaneTypesService.GetByIdAsync(id));
            }
            catch (NotExistException)
            {
                return NotFound();
            }
        }
        
        // POST: api/AirplaneTypes
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]AirplaneTypeDto dto)
        {
            try
            {
                var createdId = await _airplaneTypesService.AddAsync(dto);
                return CreatedAtAction("Get",
                    await _airplaneTypesService.GetByIdAsync(createdId));
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
        
        // PUT: api/AirplaneTypes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]AirplaneTypeDto dto)
        {
            try
            {
                await _airplaneTypesService.UpdateAsync(id, dto);
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
                await _airplaneTypesService.DeleteAsync(id);
                return NoContent();
            }
            catch (NotExistException)
            {
                return NotFound();
            }
        }
    }
}
