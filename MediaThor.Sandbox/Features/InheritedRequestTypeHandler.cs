namespace MediaThor.Sandbox.Features;

public abstract record AbstractInheritedRequestTypeQuery(string ApiKey);

public sealed record InheritedRequestTypeQuery(string ApiKey) : AbstractInheritedRequestTypeQuery(ApiKey), IRequest<string>;

public sealed class InheritedRequestTypeHandler
    : IRequestHandler<InheritedRequestTypeQuery, string>
{
    public Task<string> HandleAsync(InheritedRequestTypeQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult($"Key : '{request.ApiKey}'");
    }
}