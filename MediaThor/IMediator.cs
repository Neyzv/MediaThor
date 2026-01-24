using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MediaThor
{
    public interface IMediator
    {
        /// <summary>
        /// Send a request that needs to be handled, and retrieve its result from its appropriated Handler.
        /// </summary>
        /// <param name="request">The request to send.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <typeparam name="TResponse">The type of the response retrieved from the request.</typeparam>
        /// <returns>A task that contains the handler result.</returns>
        Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default);

        /// <summary>
        /// Send a request that needs to be handled.
        /// </summary>
        /// <param name="request">The request to send.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <typeparam name="TRequest">The type of the request to send.</typeparam>
        /// <returns></returns>
        Task Send<TRequest>(TRequest request, CancellationToken cancellationToken = default)
            where TRequest : IRequest;
        
        /// <summary>
        /// Send a streamable request that needs to be handled, and retrieve its result from its appropriated Handler.
        /// </summary>
        /// <param name="request">The request to send.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <typeparam name="TResponse">The streamable result type retrieved from the request.</typeparam>
        /// <returns>An async enumerable which contains the handler results.</returns>
        IAsyncEnumerable<TResponse> CreateStream<TResponse>(IStreamRequest<TResponse> request, CancellationToken cancellationToken = default);
    }
}

