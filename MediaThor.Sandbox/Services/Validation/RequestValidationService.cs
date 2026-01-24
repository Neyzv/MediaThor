using FluentValidation;

namespace MediaThor.Sandbox.Services.Validation;

public sealed class RequestValidationService(IServiceProvider provider)
    : IRequestValidationService
{
    public void ValidateRequest<TRequest>(TRequest request)
    {
        if (provider.GetServices<IValidator<TRequest>>().ToArray() is not { Length: > 0 } validators)
            return;
        
        var context = new ValidationContext<TRequest>(request);

        var failures = validators
            .Select(v => v.Validate(context))
            .SelectMany(r => r.Errors)
            .Where(f => f is not null)
            .ToList();

        if (failures.Count is not 0)
            throw new ValidationException(failures);
    }
}