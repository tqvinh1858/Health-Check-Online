using Azure.Messaging.ServiceBus;
using BHEP.Contract.Services.V3.CoinTransaction;
using BHEP.Infrastructure.ServiceBus.DependencyInjection.Options;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace BHEP.Infrastructure.ServiceBus.DeadLetterQueue;
public class CreateCoinTransactionDLQ : BackgroundService
{
    private readonly ServiceBusOptions serviceBusOptions;
    private readonly ServiceBusClient client;
    private readonly ServiceBusProcessor processorDLQ;
    public CreateCoinTransactionDLQ(IOptions<ServiceBusOptions> options)
    {
        serviceBusOptions = options.Value;
        client = new ServiceBusClient(serviceBusOptions.ConnectionString);

        // Processor for DeadLetterQueue
        processorDLQ = client.CreateProcessor(
            serviceBusOptions.QueueName,
            new ServiceBusProcessorOptions
            {
                SubQueue = SubQueue.DeadLetter
            });
    }

    public override async Task StartAsync(CancellationToken cancellationToken)
    {
        try
        {
            processorDLQ.ProcessMessageAsync += DeadLetterMessageHandler;
            // add handler to process any errors
            processorDLQ.ProcessErrorAsync += ErrorHandler;

            // start processing 
            await processorDLQ.StartProcessingAsync();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        await processorDLQ.StopProcessingAsync(cancellationToken);
        await processorDLQ.DisposeAsync();
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Task.Yield();
    }


    // handle received messages
    private async Task DeadLetterMessageHandler(ProcessMessageEventArgs args)
    {
        try
        {
            string messageBody = args.Message.Body.ToString();
            // Deserialize the CreateUser object
            var request = JsonConvert.DeserializeObject<Command.CreateCoinTransactionCommand>(messageBody);

            // Process the CreateUser object (e.g., save to database)
            Console.WriteLine(request);
            await args.CompleteMessageAsync(args.Message);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    // handle any errors when receiving messages
    private Task ErrorHandler(ProcessErrorEventArgs args)
    {
        Console.WriteLine(args.Exception.ToString());
        return Task.CompletedTask;
    }
}
