﻿using System;
using System.IO;
using System.Threading.Tasks;
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
        private DiscordClient _discordClient;

        public Functions()
        {

        }

        public Functions(ILogger logger, DiscordClient discordClient)
        {
            _logger = logger;
            _discordClient = discordClient;
        }

        // This function will get triggered/executed when a new message is written 
        // on an Azure Queue called queue.
        public async Task ProcessTodoActionMessageAsync([QueueTrigger("todo-done-queue")] string message)
        {
            //_logger.LogInformation(message);
            //TodoActionMessageQueue msg = JsonConvert.DeserializeObject<TodoActionMessageQueue>(message);
            TodoActionMessageQueue msg = new TodoActionMessageQueue
            {
                CorrelationId = Guid.NewGuid().ToString(),
                TodoName = "Développer un webjob"
            };
            try
            {
                _discordClient = new DiscordClient("https://discordapp.com/api/webhooks/568553400803786778/27l9zIA1D8BbntAhcNfkwzMJ47euGm95OyrA_j9qtItFPg3bC156lDOHOFwwtTJFfgzk");
                bool res = await _discordClient.SendMessageAsync(GetSlackMessage(msg));
            }
            catch (Exception e)
            {
                //_logger.LogError(e, "Une erreur est survenue lors de l'envoi au webhook :(");
            }
        }

        private DiscordMessage GetSlackMessage(TodoActionMessageQueue message)
        {
            return new DiscordMessage()
            {
                Content = $"A todo was completed",
                Embeds = new DiscordEmbed[]
                {
                    new DiscordEmbed
                    {
                        Title =  $"{message.TodoName}",
                        Color =  $"14177041",
                        Description =  $"Correlation id is: {message.CorrelationId}"
                    }
                }

            };
        }
    }
}