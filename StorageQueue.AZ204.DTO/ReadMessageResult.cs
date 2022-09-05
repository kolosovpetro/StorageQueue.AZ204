namespace StorageQueue.AZ204.DTO;

public record ReadMessageResult(
    string Id,
    string MessageText,
    string InsertedOn,
    string ExpiresOn,
    long DequeueCount);