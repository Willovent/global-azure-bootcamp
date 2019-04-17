using System.IO;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Todo.Core;
using Todo.Core.Clients;

namespace Todo.WebJob
{
    public class Functions
    {
        private readonly ILogger _logger;
        private readonly SlackClient _slackClient;

        public Functions(ILogger logger, SlackClient slackClient)
        {
            _logger = logger;
            _slackClient = slackClient;
        }

        // This function will get triggered/executed when a new message is written 
        // on an Azure Queue called queue.
        public void ProcessTodoActionMesage([QueueTrigger("serbian-queue")] string message)
        {
            _logger.LogInformation(message);
            TodoActionMessageQueue msg = JsonConvert.DeserializeObject<TodoActionMessageQueue>(message);
            _slackClient.SendMessage(GetSlackMessage(msg));
        }

        private SlackMessage GetSlackMessage(TodoActionMessageQueue message)
        {
            return new SlackMessage();
        }
    }
}
