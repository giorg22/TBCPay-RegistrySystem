using Application.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PeopleController : ControllerBase
    {
        private readonly PersonService _service;

        public PeopleController(PersonService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var person = await _service.GetByIdAsync(id);
            return person == null ? NotFound() : Ok(person);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Person person)
        {
            await _service.AddPersonAsync(person);
            return CreatedAtAction(nameof(GetById), new { id = person.Id }, person);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Person person)
        {
            if (id != person.Id) return BadRequest();
            await _service.UpdatePersonAsync(person);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeletePersonAsync(id);
            return NoContent();
        }
    }
}
