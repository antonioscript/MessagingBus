using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Producer.API.Services;

namespace Producer.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class QueueController : ControllerBase
{
    private readonly IQueueService _queueService;

    public QueueController(IQueueService queueService)
    {
        _queueService = queueService;
    }

    [HttpPost]
    public async Task<IActionResult> EnqueueAsync([FromBody] JsonElement jsonElement)
    {
        if (!jsonElement.TryGetProperty("accountId", out var _) ||
            !jsonElement.TryGetProperty("personType", out var _))
        {
            return BadRequest(new { message = "Required fields 'accountId' or 'personType' are missing." });
        }


        var success = await _queueService.SendMessageAsync(jsonElement);

        if (!success)
            return StatusCode(500, new { message = "Failed to enqueue message to SQS." });

        return Ok(new { message = "Message successfully enqueued to SQS." });
    }
}
