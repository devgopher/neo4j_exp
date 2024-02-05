using neo4j_web;

public interface IPersonRepository
{
    /// <summary>
    /// Searches the name of the person.
    /// </summary>
    Task<List<Dictionary<string, object>>> SearchPersonsByName(string searchString);

    /// <summary>
    /// Adds a new person
    /// </summary>
    Task<bool> AddPerson(Person? person);

    /// <summary>
    /// Get count of persons
    /// </summary>
    Task<long> GetPersonCount();
    
    /// <summary>
    /// Sets an occupation for a person
    /// </summary>
    Task SetOccupation(Person? person, Occupation? occupation);
}