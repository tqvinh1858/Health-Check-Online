﻿namespace BHEP.Infrastructure.RabbitMQ.DependencyInjection.Options;

public record MasstransitOptions
{
    public string Host { get; set; }
    public string VHost { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string ExchangeName { get; set; }
    public string ExchangeType { get; set; }
    public string SmsQueueName { get; set; }
    public string EmailQueueName { get; set; }
}
