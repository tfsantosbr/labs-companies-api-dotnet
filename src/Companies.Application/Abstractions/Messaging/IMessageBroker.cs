namespace Companies.Application.Abstractions.Messaging;

public interface IMessageBroker
{
    Task PostMessage<TMessage>(TMessage message);
}
