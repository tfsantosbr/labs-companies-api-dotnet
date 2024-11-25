using Companies.Application.Abstractions.Results;

namespace Companies.Application.Abstractions.Handlers;

public abstract class AbstractHandler
{
    protected static Result SuccessResult() =>
        Result.Success();

    protected static Result ErrorResult(Notification[] notifications) =>
        Result.Error(notifications);

    protected static Result ErrorResult(Notification notification) =>
        Result.Error(notification);

    protected static Result NotFoundResult(Notification notification) =>
        Result.NotFound(notification);
}

public abstract class AbstractHandler<TData> where TData : class
{
    protected Result<TData> SuccessResult(TData data) =>
        Result<TData>.Success(data);

    protected Result<TData> ErrorResult(Notification[] notifications) =>
        Result<TData>.Error(notifications);

    protected Result<TData> ErrorResult(Notification notification) =>
        Result<TData>.Error(notification);

    protected Result<TData> NotFoundResult(Notification notification) =>
        Result<TData>.NotFound(notification);
}
