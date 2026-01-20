using System.Runtime.CompilerServices;

namespace MediaThor.Sandbox.Features;

public sealed record StreamedQuery(byte Amount) : IStreamRequest<int>;

public sealed class StreamedHandler
    : IStreamRequestHandler<StreamedQuery, int>
{
    public async IAsyncEnumerable<int> HandleAsync(StreamedQuery request, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        for (var i = 0; i < request.Amount; i++)
        {
            cancellationToken.ThrowIfCancellationRequested();
            yield return Random.Shared.Next(1, 1_000);
            await Task.Yield();
        }
    }
}