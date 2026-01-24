namespace MediaThor.Sandbox.Features;

public sealed record NoContentQuery : IRequest;

public sealed class NoContentHandler
    : IRequestHandler<NoContentQuery>
{
    public Task HandleAsync(NoContentQuery request, CancellationToken cancellationToken)
    {
        return Task.Delay(3_000, cancellationToken);
    }
}