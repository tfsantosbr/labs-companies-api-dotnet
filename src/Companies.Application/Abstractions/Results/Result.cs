namespace Companies.Application.Abstractions.Results;

public record Result
{
    protected Result()
    {
    }

    protected Result(Notification notification) =>
        Notifications = [notification];

    protected Result(Notification[] notifications) =>
        Notifications = notifications;

    public Notification[] Notifications { get; } = [];
    public bool IsSuccess => Notifications.Length == 0;
    public bool IsFailure => !IsSuccess;

    public static ErrorResult Error(Notification notification) => new(notification);
    public static ErrorResult Error(Notification[] notifications) => new(notifications);
    public static NotFoundResult NotFound(Notification notification) => new(notification);
    public static Result Success() => new();
    public static Result<TData> Success<TData>(TData data) => new(data);
}

public record Result<TData> : Result
{
    public TData? Data { get; }

    internal Result(TData data)
    {
        Data = data;
    }

    internal Result(Notification notification)
        : base(notification)
    {
    }

    internal Result(Notification[] notifications)
        : base(notifications)
    {
    }

    public static new ErrorResult<TData> Error(Notification notification) => new(notification);
    public static new ErrorResult<TData> Error(Notification[] notifications) => new(notifications);
    public static new NotFoundResult<TData> NotFound(Notification notification) => new(notification);
    public static Result<TData> Success(TData data) => new(data);
}