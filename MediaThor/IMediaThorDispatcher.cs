using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MediaThor
{
    public interface IMediaThorDispatcher
    {
        /// <summary>
        /// Dispatch a request to its appropriated handler.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        /// <param name="request">The request that needs to be dispatched.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <typeparam name="TResponse">The type of the response retrieved from the request.</typeparam>
        /// <returns>The result of the pipeline execution.</returns>
        Task<TResponse> DispatchAsync<TResponse>(IServiceProvider serviceProvider, IRequest<TResponse> request,
            CancellationToken cancellationToken);
        
        /// <summary>
        /// Dispatch a request to its appropriated handler.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        /// <param name="request">The request that needs to be dispatched.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <typeparam name="TRequest">The type of the request to submit.</typeparam>
        Task DispatchAsync<TRequest>(IServiceProvider serviceProvider, TRequest request, CancellationToken cancellationToken)
            where TRequest : IRequest;

        /// <summary>
        /// Dispatch a streameable request to its appropriated handler.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        /// <param name="request">The request that needs to be dispatched</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <typeparam name="TResponse">The streamable result type retrieved from the request.</typeparam>
        /// <returns>The streamable result of the pipeline execution.</returns>
        IAsyncEnumerable<TResponse> DispatchAsync<TResponse>(IServiceProvider serviceProvider,
            IStreamRequest<TResponse> request, CancellationToken cancellationToken);
    }
}