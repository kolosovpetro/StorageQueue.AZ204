using System.Threading.Tasks;
using StorageQueue.AZ204.DTO;

namespace StorageQueue.AZ204.Sender;

public interface IStorageQueueSender
{
    Task<SendMessageResponse> SendMessageAsync(string message);
}