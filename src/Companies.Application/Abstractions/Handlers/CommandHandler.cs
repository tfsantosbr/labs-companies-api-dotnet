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

public abstract class CommandHandler<T> where T : class
{
    protected Result<T> SuccessResult(T data) =>
        Result<T>.Success(data);

    protected Result<T> ErrorResult(Notification[] notifications) =>
        Result<T>.Error(notifications);

    protected Result<T> ErrorResult(Notification notification) =>
        Result<T>.Error(notification);
}