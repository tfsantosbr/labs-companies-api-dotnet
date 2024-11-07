using Companies.Application.Abstractions.Results;

namespace Companies.Application.Abstractions.Validations;

public interface ICommandValidator<in T>
{
    Result Validate(T instance);
}
