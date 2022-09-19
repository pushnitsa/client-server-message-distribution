using Microsoft.AspNetCore.Mvc;
using Server.Host;
using WebApp.Models;

namespace WebApp.Controllers;

[Route("api/broadcast")]
[ApiController]
public class BroadcastController : ControllerBase
{
    private readonly IBroadcast _broadcastService;

    public BroadcastController(IBroadcast broadcastService)
    {
        _broadcastService = broadcastService;
    }

    [HttpPost("send")]
    public async Task<IActionResult> SendMessageToClients([FromBody] MessageModel messageModel)
    {

        await _broadcastService.BroadcastAsync(messageModel.Message);
        return Ok();
    }
}
