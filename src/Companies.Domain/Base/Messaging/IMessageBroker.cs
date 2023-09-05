namespace Companies.Domain.Base.Messaging;

public interface IMessageBroker
{
    Task PostMessage<TMessage>(TMessage message);
}
