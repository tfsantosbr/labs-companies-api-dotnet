namespace Companies.Application.Abstractions.Results;

public record NotFoundResult(Notification Notification) : Result(Notification);

public record NotFoundResult<TValue>(Notification Notification) : Result<TValue>(Notification);
