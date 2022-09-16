using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[Route("api/broadcast")]
[ApiController]
public class BroadcastController : ControllerBase
{
    public BroadcastController()
    {

    }

    [HttpGet("send")]
    public IActionResult SendMessageToClients()
    {

        return Ok();
    }
}

