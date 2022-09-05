using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StorageQueue.AZ204.DTO;

namespace StorageQueue.AZ204.Sender.Controllers;

[ApiController]
[Route("[controller]")]
public class StorageQueueSenderController : ControllerBase
{
    private readonly IStorageQueueSender _storageQueueSender;

    public StorageQueueSenderController(IStorageQueueSender storageQueueSender)
    {
        _storageQueueSender = storageQueueSender;
    }

    [HttpPost]
    public async Task<IActionResult> SendMessageToStorageQueueAsync([FromBody] Message message)
    {
        var jsonString = JsonSerializer.Serialize(message);
        var result = await _storageQueueSender.SendMessageAsync(jsonString);

        if (!result.Success)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }
}