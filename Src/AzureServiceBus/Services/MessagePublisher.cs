using Azure.Messaging.ServiceBus;
using BHEP.Infrastructure.ServiceBus.DependencyInjection.Options;
using BHEP.Infrastructure.ServiceBus.Services.IServices;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace BHEP.Infrastructure.ServiceBus.Services;
public class MessagePublisher : IMessagePublisher
{
    private readonly ServiceBusClient client;
    private readonly ServiceBusOptions serviceBusOptions;
    private ServiceBusSender sender;
    public MessagePublisher(IOptions<ServiceBusOptions> options)
    {
        serviceBusOptions = options.Value;
        client = new ServiceBusClient(serviceBusOptions.ConnectionString);
    }

    public async Task Publish(string raw, string queueOrTopicName, Dictionary<string, object> properties = null)
    {
        // Create ServiceBusClient
        sender = client.CreateSender(queueOrTopicName);
        // create a batch 
        using ServiceBusMessageBatch messageBatch = await sender.CreateMessageBatchAsync();

        // create a message
        var message = CreateMessage(raw);

        // try adding a message to the batch
        messageBatch.TryAddMessage(message);

        try
        {
            // Use the producer client to send the batch of messages to the Service Bus queue
            await sender.SendMessagesAsync(messageBatch);
        }
        finally
        {
            await DisposeAsync();
        }
    }

    public async Task Publish(List<string> listMessage, string queueOrTopicName, Dictionary<string, object> properties = null)
    {
        sender = client.CreateSender(queueOrTopicName);
        // create a batch 
        using ServiceBusMessageBatch messageBatch = await sender.CreateMessageBatchAsync();

        for (int i = 1; i <= listMessage.Count; i++)
        {
            var message = CreateMessage(listMessage[i], properties);

            // try adding a message to the batch
            if (!messageBatch.TryAddMessage(message))
            {
                // if it is too large for the batch
                throw new Exception($"The message {i} is too large to fit in the batch.");
            }
        }
        try
        {
            // Use the producer client to send the batch of messages to the Service Bus queue
            await sender.SendMessagesAsync(messageBatch);
        }
        finally
        {
            await DisposeAsync();
        }
    }

    public async Task Publish<T>(T obj, string queueOrTopicName, Dictionary<string, object> properties = null)
    {
        sender = client.CreateSender(queueOrTopicName);

        // create a batch 
        using ServiceBusMessageBatch messageBatch = await sender.CreateMessageBatchAsync();

        //Serialize obj
        var body = JsonConvert.SerializeObject(obj);

        // Create Message
        var message = CreateMessage(body, properties);

        // try adding a message to the batch
        messageBatch.TryAddMessage(message);

        try
        {
            // Use the producer client to send the batch of messages to the Service Bus queue
            await sender.SendMessagesAsync(messageBatch);
        }
        finally
        {
            await DisposeAsync();
        }
    }

    private async Task DisposeAsync()
    {
        // Calling DisposeAsync on client types is required to ensure that network
        // resources and other unmanaged objects are properly cleaned up.
        await sender.DisposeAsync();
        await client.DisposeAsync();
    }

    private ServiceBusMessage CreateMessage(string messageContent, Dictionary<string, object> properties = null)
    {
        // Create new Message
        var message = new ServiceBusMessage(messageContent);

        // Add custom properties to the message
        if (properties != null)
        {
            foreach (var property in properties)
            {
                message.ApplicationProperties.Add(property.Key, property.Value);
            }
        }

        return message;
    }
}
