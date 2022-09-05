using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace StorageQueue.AZ204.Consumer.Controllers;

[ApiController]
[Route("[controller]")]
public class StorageQueueConsumerController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllEventsConsumedAsync()
    {
        var allMessages = ConsumerBackgroundService.Messages;

        return await Task.FromResult(Ok(allMessages));
    }
}