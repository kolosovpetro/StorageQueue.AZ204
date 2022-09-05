namespace StorageQueue.AZ204.DTO;

public record SendMessageResponse(string MessageId, string PopReceipt, bool Success, int StatusCode);