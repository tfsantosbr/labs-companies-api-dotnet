using Companies.Application.Abstractions.Results;
using FluentValidation;

namespace Companies.Application.Abstractions.Validations;

public class CommandValidator<T>(IValidator<T> validator) : ICommandValidator<T>
{
    public Result Validate(T instance)
    {
        var validationResult = validator.Validate(instance);

        if (validationResult.IsValid)
            return Result.Success();

        var notifications = validationResult.Errors.Select(e =>
            new Error(e.PropertyName, e.ErrorMessage)
        ).ToArray();

        return Result.Error(notifications);
    }
}
