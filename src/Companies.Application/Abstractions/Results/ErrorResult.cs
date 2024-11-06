namespace Companies.Application.Abstractions.Results;

public record ErrorResult : Result
{
    public ErrorResult(Notification notification) : base(notification)
    {
    }

    public ErrorResult(Notification[] notifications) : base(notifications)
    {
    }
}

public record ErrorResult<TValue> : Result<TValue>
{
    public ErrorResult(Notification notification) : base(notification)
    {
    }

    public ErrorResult(Notification[] notifications) : base(notifications)
    {
    }
}