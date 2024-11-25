using Companies.Application.Abstractions.Results;

namespace Companies.Application.Abstractions.Handlers;

public abstract class CommandHandler
{
    protected static Result SuccessResult() =>
        Result.Success();

    protected static Result ErrorResult(Notification[] notifications) =>
        Result.Error(notifications);

    protected static Result ErrorResult(Notification notification) =>
        Result.Error(notification);
}

public abstract class CommandHandler<TData> where TData : class
{
    protected Result<TData> SuccessResult(TData data) =>
        Result<TData>.Success(data);

    protected Result<TData> ErrorResult(Notification[] notifications) =>
        Result<TData>.Error(notifications);

    protected Result<TData> ErrorResult(Notification notification) =>
        Result<TData>.Error(notification);
}