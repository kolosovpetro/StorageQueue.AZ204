using System;
using System.Threading.Tasks;
using Azure.Storage.Queues;
using StorageQueue.AZ204.DTO;

namespace StorageQueue.AZ204.Sender;

public class StorageQueueSender : IStorageQueueSender
{
    private const string QueueName = "pkolosov-storage-queue";
    private const string EnvironmentKey = "AZURE_STORAGE_ACCOUNT";
    private readonly QueueClient _queueClient;

    public StorageQueueSender()
    {
        var connectionString = Environment.GetEnvironmentVariable(EnvironmentKey) ??
                               throw new InvalidOperationException(
                                   $"Environment variable is not set {nameof(EnvironmentKey)}");

        var queueOptions = new QueueClientOptions
        {
            MessageEncoding = QueueMessageEncoding.Base64
        };

        _queueClient = new QueueClient(connectionString, QueueName, queueOptions);
        _queueClient.CreateIfNotExists();
    }

    public async Task<SendMessageResponse> SendMessageAsync(string message)
    {
        var exists = await _queueClient.ExistsAsync();

        if (!exists.Value)
        {
            return new SendMessageResponse(string.Empty, PopReceipt: string.Empty, Success: false, StatusCode: 400);
        }

        var response = await _queueClient.SendMessageAsync(message);

        var result = new SendMessageResponse(
            response.Value.MessageId,
            response.Value.PopReceipt,
            Success: true,
            StatusCode: 200);

        return result;
    }
}