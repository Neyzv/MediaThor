using System.Collections.Generic;
using System.Threading;

namespace MediaThor
{
    /// <summary>
    /// An interface that should be implemented to handle a streamable request and provide its result.
    /// </summary>
    /// <typeparam name="TRequest">The type of the request.</typeparam>
    /// <typeparam name="TResponse">The type of the streamable response.</typeparam>
    public interface IStreamRequestHandler<in TRequest, out TResponse>
        where TRequest : IStreamRequest<TResponse>
    {
        /// <summary>
        /// Provide a result for the associated <see cref="TRequest"/>.
        /// </summary>
        /// <param name="request">The instance of the request that should be treated.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The result of this request.</returns>
        IAsyncEnumerable<TResponse> HandleAsync(TRequest request, CancellationToken cancellationToken);
    }
}