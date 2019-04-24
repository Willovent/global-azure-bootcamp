using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Todo.Core.Clients
{
    public class TodoQueueClient
    {
        private readonly CloudQueue _queue;

        public TodoQueueClient(string connectionString)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);

            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();
            _queue = queueClient.GetQueueReference("todo-done-queue");
        }

        public async Task SendMessageAsync(TodoActionMessageQueue message)
        {
            await _queue.CreateIfNotExistsAsync();
            await _queue.AddMessageAsync(new CloudQueueMessage(JsonConvert.SerializeObject(message)));
        }
    }
}
