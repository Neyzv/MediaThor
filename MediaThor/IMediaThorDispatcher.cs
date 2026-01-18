using System;
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
        Task<TResponse> Dispatch<TResponse>(IServiceProvider serviceProvider, IRequest<TResponse> request,
            CancellationToken cancellationToken);
    }
}