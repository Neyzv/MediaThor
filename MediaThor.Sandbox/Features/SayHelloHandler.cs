using FluentValidation;

namespace MediaThor.Sandbox.Features;

public sealed record SayHelloQuery(string Name) : IRequest<string>;

public sealed class SayHelloHandler
    : IRequestHandler<SayHelloQuery, string>
{
    public Task<string> HandleAsync(SayHelloQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult($"Hello {request.Name} !");
    }
}

public sealed class SayHelloValidator
    : AbstractValidator<SayHelloQuery>
{
    public SayHelloValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required");
    }
}