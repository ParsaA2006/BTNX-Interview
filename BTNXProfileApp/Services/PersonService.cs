using BTNXProfileApp.Models;

namespace BTNXProfileApp.Services;

public interface IPersonService
{
    Task<Person?> GetPersonAsync(int id);
    Task<IEnumerable<Person>> GetAllPeopleAsync();
    Task<Person> CreatePersonAsync(Person person);
    Task<Person> UpdatePersonAsync(Person person);
    Task<bool> DeletePersonAsync(int id);
}

public class PersonService : IPersonService
{
    private readonly List<Person> _people = new();
    private int _nextId = 1;

    public Task<Person?> GetPersonAsync(int id)
    {
        return Task.FromResult(_people.FirstOrDefault(p => p.Id == id));
    }

    public Task<IEnumerable<Person>> GetAllPeopleAsync()
    {
        return Task.FromResult(_people.AsEnumerable());
    }

    public Task<Person> CreatePersonAsync(Person person)
    {
        person.Id = _nextId++;
        _people.Add(person);
        return Task.FromResult(person);
    }

    public Task<Person> UpdatePersonAsync(Person person)
    {
        var existingPerson = _people.FirstOrDefault(p => p.Id == person.Id);
        if (existingPerson == null)
            throw new KeyNotFoundException($"Person with ID {person.Id} not found.");

        existingPerson.FirstName = person.FirstName;
        existingPerson.LastName = person.LastName;
        existingPerson.Email = person.Email;
        existingPerson.PhoneNumber = person.PhoneNumber;

        return Task.FromResult(existingPerson);
    }

    public Task<bool> DeletePersonAsync(int id)
    {
        var person = _people.FirstOrDefault(p => p.Id == id);
        if (person == null)
            return Task.FromResult(false);

        _people.Remove(person);
        return Task.FromResult(true);
    }
} 