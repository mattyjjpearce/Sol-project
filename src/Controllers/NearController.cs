using blockchain_test.Services;
using Microsoft.AspNetCore.Mvc;

namespace BlockchainApi.Controllers;

[ApiController]
[Route("/api/blockchain")]
public class NearController : ControllerBase
{
    private readonly TransferTokensService _transferTokenService;

    public NearController(TransferTokensService transferTokenService)
    {
        _transferTokenService = transferTokenService;
    }

    

    [HttpGet("transaction")]
    public async Task<IActionResult> GetFtMetadata()
    {
        var metadata =  _transferTokenService.TransferTokens();

        return Ok();
    }



    // [HttpGet("{id}/balance")]
    // public async Task<IActionResult> GetBalance(string id)
    // {
    //     var balance = await _nearService.GetFtBalanceAsync(id);
    //     return Ok(balance);
    // }

    // [HttpPost("{id}/account")]
    // public async Task<IActionResult> CreateAccount(string subAccountName, string masterAccountName)
    // {

    // }

}

