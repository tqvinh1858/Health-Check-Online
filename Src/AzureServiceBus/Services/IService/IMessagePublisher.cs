namespace BHEP.Infrastructure.ServiceBus.Services.IServices;
public interface IMessagePublisher
{
    Task Publish(string raw, string queueOrTopicName, Dictionary<string, object> properties = null);
    Task Publish(List<string> listMessage, string queueOrTopicName, Dictionary<string, object> properties = null);
    Task Publish<T>(T obj, string queueOrTopicName, Dictionary<string, object> properties = null);
}
