namespace BHEP.Infrastructure.ServiceBus.DependencyInjection.Options;
public class ServiceBusOptions
{
    public bool Enable { get; set; }
    public string ConnectionString { get; set; }
    public string QueueName { get; set; }
    public string TopicName { get; set; }
    public string CreateCoinTransactionSubscription { get; set; }
    public string SendEmailSubscription { get; set; }
}
