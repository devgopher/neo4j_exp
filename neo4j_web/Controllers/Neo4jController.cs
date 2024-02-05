using Microsoft.AspNetCore.Mvc;

namespace neo4j_web.Controllers;

[ApiController]
[Route("/v1")]
public class Neo4jController
{
    private readonly IPersonRepository _repository;

    public Neo4jController(IPersonRepository repository) => _repository = repository;
    
    [HttpGet("[action]")]
    public async Task<List<Dictionary<string, object>>> SearchPersonsByName([FromQuery]string searchString)
        => await _repository.SearchPersonsByName(searchString);

    [HttpGet("[action]")]
    public async Task<bool> AddPerson([FromQuery]Person? person)
        => await _repository.AddPerson(person);

    [HttpGet("[action]")]
    public async Task<long> GetPersonCount() 
        => await _repository.GetPersonCount();

    [HttpGet("[action]")]
    public async Task SetOccupation([FromQuery] Person? person, [FromQuery] Occupation? occupation)
        => await _repository.SetOccupation(person, occupation);
}