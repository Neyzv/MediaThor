using MediaThor.Sandbox.Services.Validation;

namespace MediaThor.Sandbox.Behaviors;

[MediaThorPipePriority(0)]
public sealed class ValidationBehavior<TRequest>(IRequestValidationService requestValidationService)
    : IPipelineBehavior<TRequest>
    where TRequest : IRequest
{
    public Task HandleAsync(TRequest request, RequestHandlerDelegate next, CancellationToken cancellationToken)
    {
        requestValidationService.ValidateRequest(request);

        return next(cancellationToken);
    }
}

[MediaThorPipePriority(0)]
public sealed class ValidationBehavior<TRequest, TResponse>(IRequestValidationService requestValidationService)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    public Task<TResponse> HandleAsync(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        requestValidationService.ValidateRequest(request);

        return next(cancellationToken);
    }
}
