using Azure.Messaging.ServiceBus;
using BHEP.Contract.Services.V3.CoinTransaction;
using BHEP.Infrastructure.ServiceBus.DependencyInjection.Options;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace BHEP.Infrastructure.ServiceBus.Consumer;
public class CreateCoinTransactionConsumer : BackgroundService
{
    private readonly ServiceBusOptions serviceBusOptions;
    private readonly ServiceBusClient client;
    private readonly ServiceBusProcessor processor;
    public CreateCoinTransactionConsumer(IOptions<ServiceBusOptions> options)
    {
        serviceBusOptions = options.Value;
        client = new ServiceBusClient(serviceBusOptions.ConnectionString);

        processor = client.CreateProcessor(
            serviceBusOptions.QueueName,
            new ServiceBusProcessorOptions()
            {
                AutoCompleteMessages = false,
                MaxConcurrentCalls = 3      //  Max messages that the processor can process same time
            });
    }

    public override async Task StartAsync(CancellationToken cancellationToken)
    {
        try
        {
            processor.ProcessMessageAsync += MessageHandler;

            // add handler to process any errors
            processor.ProcessErrorAsync += ErrorHandler;

            // start processing 
            await processor.StartProcessingAsync();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        await processor.StopProcessingAsync(cancellationToken);
        await processor.DisposeAsync();
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Task.Yield();
    }


    // handle received messages
    private async Task MessageHandler(ProcessMessageEventArgs args)
    {
        try
        {
            string messageBody = args.Message.Body.ToString();
            //throw new Exception("Move to DLQ");

            // Deserialize the CreateUser object
            var request = JsonConvert.DeserializeObject<Command.CreateCoinTransactionCommand>(messageBody);

            // Process the CreateUser object (e.g., save to database)
            Console.WriteLine(request);

            await args.CompleteMessageAsync(args.Message); // If the option `AutoCompleteMessages` is off
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
