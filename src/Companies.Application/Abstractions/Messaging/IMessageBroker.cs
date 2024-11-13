namespace Companies.Application.Abstractions.Messaging;

public interface IMessageBroker
{
    Task PostMessageAsync<TMessage>(TMessage message);
}
