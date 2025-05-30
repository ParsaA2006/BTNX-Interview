using Microsoft.AspNetCore.Mvc;
using BTNXProfileApp.Models;
using BTNXProfileApp.Services;

namespace BTNXProfileApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PersonController : ControllerBase
{
    private readonly IPersonService _personService;

    public PersonController(IPersonService personService)
    {
        _personService = personService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Person>>> GetAll()
    {
        var people = await _personService.GetAllPeopleAsync();
        return Ok(people);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Person>> GetById(int id)
    {
        var person = await _personService.GetPersonAsync(id);
        if (person == null)
            return NotFound();

        return Ok(person);
    }

    [HttpPost]
    public async Task<ActionResult<Person>> Create(Person person)
    {
        var createdPerson = await _personService.CreatePersonAsync(person);
        return CreatedAtAction(nameof(GetById), new { id = createdPerson.Id }, createdPerson);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Person>> Update(int id, Person person)
    {
        if (id != person.Id)
            return BadRequest();

        try
        {
            var updatedPerson = await _personService.UpdatePersonAsync(person);
            return Ok(updatedPerson);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var result = await _personService.DeletePersonAsync(id);
        if (!result)
            return NotFound();

        return NoContent();
    }
} 