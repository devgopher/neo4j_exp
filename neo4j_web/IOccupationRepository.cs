using neo4j_web;

public interface IOccupationRepository
{
    /// <summary>
    /// Searches the name of the Occupation.
    /// </summary>
    Task<List<Dictionary<string, object>>> SearchOccupationsByName(string searchString);

    /// <summary>
    /// Adds a new Occupation
    /// </summary>
    Task<bool> AddOccupation(Occupation? occupation);

    /// <summary>
    /// Get count of Occupations
    /// </summary>
    Task<long> GetOccupationCount();
}