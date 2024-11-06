namespace Companies.Application.Base.Messaging;

public interface IMessageBroker
{
    Task PostMessage<TMessage>(TMessage message);
}
