using Microsoft.AspNetCore.Mvc;

namespace neo4j_web.Controllers;

[ApiController]
[Route("/v1")]
public class OccupationController
{
    private readonly IOccupationRepository _repository;

    public OccupationController(IOccupationRepository repository) => _repository = repository;
    
    [HttpGet("[action]")]
    public async Task<List<Dictionary<string, object>>> SearchOccupationsByName([FromQuery]string searchString)
        => await _repository.SearchOccupationsByName(searchString);

    [HttpGet("[action]")]
    public async Task<bool> AddOccupation([FromQuery]Occupation? Occupation)
        => await _repository.AddOccupation(Occupation);

    [HttpGet("[action]")]
    public async Task<long> GetOccupationCount() 
        => await _repository.GetOccupationCount();
}