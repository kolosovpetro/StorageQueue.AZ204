using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StorageQueue.AZ204.DTO;

namespace StorageQueue.AZ204.Consumer;

public class ConsumerBackgroundService : BackgroundService
{
    private readonly IQueueStorageConsumer _queueStorageConsumer;
    private readonly ILogger<ReadMessageResult> _logger;

    public static List<ReadMessageResult> Messages { get; } = new();

    public ConsumerBackgroundService(
        IQueueStorageConsumer queueStorageConsumer,
        ILogger<ReadMessageResult> logger)
    {
        _queueStorageConsumer = queueStorageConsumer;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Background service has been started.");

        while (!stoppingToken.IsCancellationRequested)
        {
            var message = await _queueStorageConsumer.ReadMessageAsync();

            if (message == null)
            {
                _logger.LogInformation("Background service received null message. Continues.");
                await Task.Delay(2000, stoppingToken);
                continue;
            }

            _logger.LogInformation($"Message received: {message.MessageText}");

            var contains = Messages.Any(x => x.Id == message.Id);
            
            if (!contains)
            {
                Messages.Add(message);
            }

            await Task.Delay(2000, stoppingToken);
        }
    }
}