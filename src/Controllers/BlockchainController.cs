using Microsoft.AspNetCore.Mvc;

namespace BlockchainApi.Controllers;

[ApiController]
[Route("/api/blockchain")]
public class BlockchainController : ControllerBase
{

   
    
    [HttpGet]
    public async Task<ActionResult<string>> Get(){
        try
        {
            return Ok("hello, world");
        } 
        catch
        {
            return StatusCode(500);
        }
        
    }
}