using Application.DTOs;
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

        [HttpPost("{personId}/relationships")]
        public async Task<IActionResult> AddRelationship(int personId, [FromBody] RelationshipDto request)
        {
            await _service.AddRelationshipAsync(personId, request.RelatedPersonId, request.RelationshipType);
            return Ok("Relationship added successfully.");
        }

        [HttpDelete("{personId}/relationships/{relatedPersonId}")]
        public async Task<IActionResult> RemoveRelationship(int personId, int relatedPersonId)
        {
            await _service.RemoveRelationshipAsync(personId, relatedPersonId);
            return Ok("Relationship removed successfully.");
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Person>>> Search([FromQuery] string? firstName, [FromQuery] string? lastName, [FromQuery] string? personalNumber, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var people = await _service.SearchAsync(firstName, lastName, personalNumber, page, pageSize);
            return Ok(people);
        }

        [HttpPost("advanced-search")]
        public async Task<ActionResult<IEnumerable<Person>>> AdvancedSearch([FromBody] PersonSearchCriteria criteria, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var people = await _service.AdvancedSearchAsync(criteria, page, pageSize);
            return Ok(people);
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
