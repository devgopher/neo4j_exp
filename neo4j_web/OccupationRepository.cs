using neo4j_web;

public class OccupationRepository : IOccupationRepository
{
    private INeo4jDataAccess _neo4jDataAccess;

    private ILogger<OccupationRepository> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="OccupationRepository"/> class.
    /// </summary>
    public OccupationRepository(INeo4jDataAccess neo4jDataAccess, ILogger<OccupationRepository> logger)
    {
        _neo4jDataAccess = neo4jDataAccess;
        _logger = logger;
    }

    /// <summary>
    /// Searches the name of the Occupation.
    /// </summary>
    public async Task<List<Dictionary<string, object>>> SearchOccupationsByName(string searchString)
    {
        var query = @"MATCH (p:Occupation) WHERE toUpper(p.name) CONTAINS toUpper($searchString) 
                                RETURN p{ name: p.name, born: p.born } ORDER BY p.Name LIMIT 5";

        IDictionary<string, object> parameters = new Dictionary<string, object> { { "searchString", searchString } };

        return await _neo4jDataAccess.ExecuteReadDictionaryAsync(query, "p", parameters);
    }

     /// <summary>
    /// Adds a new Occupation
    /// </summary>
    public async Task<bool> AddOccupation(Occupation? occupation)
    {
        if (occupation != null && !string.IsNullOrWhiteSpace(occupation.Field) && !string.IsNullOrWhiteSpace(occupation.Specialty))
        {
            var query = @"MERGE (p:Occupation {field: $field, specialty:$specialty}) ON CREATE SET p.fromyear = $fromyear 
                            ON MATCH SET p.fromyear = $fromyear, p.updatedAt = timestamp() RETURN true";
            IDictionary<string, object> parameters = new Dictionary<string, object> 
            { 
                { "field", occupation.Field },
                { "specialty", occupation.Specialty ?? "unknown" },
                { "fromyear", occupation.FromYear }
            };
            return (await _neo4jDataAccess.ExecuteWriteTransactionAsync<bool>(query, parameters)).FirstOrDefault();
        }
        else
        {
            throw new System.ArgumentNullException(nameof(occupation), "Occupation must not be null");
        }
    }

    /// <summary>
    /// Get count of Occupations
    /// </summary>
    public async Task<long> GetOccupationCount()
    {
        var query = @"Match (p:Occupation) RETURN count(p) as OccupationCount";
        var count = await _neo4jDataAccess.ExecuteReadScalarAsync<long>(query);
        return count;
    }
}