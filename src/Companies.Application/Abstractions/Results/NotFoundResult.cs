namespace Companies.Application.Abstractions.Results;

public record NotFoundResult(Error Errors) : Result(Errors);

public record NotFoundResult<TData>(Error Errors) : Result<TData>(Errors);
