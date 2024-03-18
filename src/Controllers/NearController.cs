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

    

    [HttpGet("metadata")]
    public async Task<IActionResult> GetFtMetadata()
    {
        var metadata = await _nearService.GetFtMetadataAsync();

        return Ok(metadata);
    }

    [HttpGet("{id}/balance")]
    public async Task<IActionResult> GetBalance(string id)
    {
        Console.WriteLine(id);
        var balance = await _nearService.GetFtBalanceAsync(id);
        return Ok(balance);
    }

    // [HttpPost("{id}/account")]
    // public async Task<IActionResult> CreateAccount(string subAccountName, string masterAccountName)
    // {

    // }

}

