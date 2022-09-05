using System.Threading.Tasks;
using StorageQueue.AZ204.DTO;

namespace StorageQueue.AZ204.Consumer;

public interface IQueueStorageConsumer
{
    Task<ReadMessageResult> ReadMessageAsync();
}