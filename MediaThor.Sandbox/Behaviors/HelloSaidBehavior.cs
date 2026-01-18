using MediaThor.Sandbox.Features;

namespace MediaThor.Sandbox.Behaviors;

[MediaThorPipePriority(0)]
public sealed class HelloSaidBehavior
    : IPipelineBehavior<SayHelloQuery, string>
{
    private const char Space = ' ';

    public async Task<string> HandleAsync(SayHelloQuery request, RequestHandlerDelegate<string> next, CancellationToken cancellationToken)
    {
        if (request.Name.Contains(Space))
            throw new InvalidOperationException();

        return await next(cancellationToken);
    }
}