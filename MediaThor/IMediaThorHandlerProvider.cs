using System;

namespace MediaThor
{
    public interface IMediaThorHandlerProvider
    {
        /// <summary>
        /// Retrieve the appropriate request handler for the specified request.
        /// </summary>
        /// <param name="serviceProvider">The current instance of the service provider.</param>
        /// <param name="request">The request that needs to be handled.</param>
        /// <returns>A delegate that represent the handler of the request.</returns>
        RequestHandlerDelegate TryGetHandler(IServiceProvider serviceProvider, IRequest request);

        /// <summary>
        /// Retrieve the appropriate request handler for the specified request.
        /// </summary>
        /// <param name="serviceProvider">The current instance of the service provider.</param>
        /// <param name="request">The request that needs to be handled.</param>
        /// <typeparam name="TResponse"></typeparam>
        /// <returns>A delegate that represent the handler of the request.</returns>
        RequestHandlerDelegate<TResponse> TryGetHandler<TResponse>(IServiceProvider serviceProvider, IRequest<TResponse> request);
    }
}