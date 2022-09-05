using System;
using System.Threading.Tasks;
using Azure.Storage.Queues;
using StorageQueue.AZ204.DTO;

namespace StorageQueue.AZ204.Consumer;

public class QueueStorageConsumer : IQueueStorageConsumer
{
    private const string QueueName = "pkolosov-storage-queue";
    private const string EnvironmentKey = "AZURE_STORAGE_ACCOUNT";
    private readonly QueueClient _queueClient;

    public QueueStorageConsumer()
    {
        var connectionString = Environment.GetEnvironmentVariable(EnvironmentKey) ??
                               throw new InvalidOperationException(
                                   $"Environment variable is not set {EnvironmentKey}");

        var queueOptions = new QueueClientOptions
        {
            MessageEncoding = QueueMessageEncoding.Base64
        };

        _queueClient = new QueueClient(connectionString, QueueName, queueOptions);
        _queueClient.CreateIfNotExists();
    }


    public async Task<ReadMessageResult> ReadMessageAsync()
    {
        var result = await _queueClient.ReceiveMessageAsync();

        if (result.Value == null)
        {
            return null;
        }

        var response = new ReadMessageResult(
            result.Value.MessageId,
            result.Value.MessageText,
            result.Value.InsertedOn.ToString(),
            result.Value.ExpiresOn.ToString(),
            result.Value.DequeueCount);

        return response;
    }
}