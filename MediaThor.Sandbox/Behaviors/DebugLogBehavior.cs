using System.Diagnostics;
using MediaThor.Sandbox.Features;

namespace MediaThor.Sandbox.Behaviors;

[MediaThorPipePriority(1)]
public sealed class DebugLogBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    public Task<TResponse> HandleAsync(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        Debug.WriteLine($"Received request : '{request}'.");
        
        return next(cancellationToken);
    }
}