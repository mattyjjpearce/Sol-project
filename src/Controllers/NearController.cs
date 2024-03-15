using Microsoft.AspNetCore.Mvc;

namespace BlockchainApi.Controllers;

[ApiController]
[Route("/api/blockchain")]
public class NearController : ControllerBase
{
    private readonly NearService _nearService;

    public NearController(NearService nearService)
    {
        _nearService = nearService;
    }

    [HttpGet]
    public async Task<IActionResult> GetFtMetadata()
    {
        var metadata = await _nearService.GetFtMetadataAsync();

        return Ok(metadata);
    }
 
}