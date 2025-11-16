using TrainerApi.Events;

namespace TrainerApi.Infrastructure.Producers;

public interface IMessageBrokerProducer
{
    Task ProduceAsync<T>(T message) where T : class, IEventMessage;
}