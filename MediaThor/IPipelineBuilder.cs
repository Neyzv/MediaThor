using System;

namespace MediaThor
{
    public interface IPipelineBuilder
    {
        /// <summary>
        /// Build a request pipeline based on the provided <paramref name="request"/>.
        /// </summary>
        /// <param name="request">The request to treat.</param>
        /// <param name="serviceProvider">The service provider.</param>
        /// <param name="handler">The request handler.</param>
        /// <typeparam name="TRequest">The type of the request.</typeparam>
        /// <returns>The entry point of the execution of the request pipeline.</returns>
        RequestHandlerDelegate BuildPipeline<TRequest>(TRequest request, IServiceProvider serviceProvider, RequestHandlerDelegate handler)
            where TRequest : IRequest;
        
        /// <summary>
        /// Build a request pipeline based on the provided <paramref name="request"/>.
        /// </summary>
        /// <param name="request">The request to treat.</param>
        /// <param name="serviceProvider">The service provider.</param>
        /// <param name="handler">The request handler.</param>
        /// <typeparam name="TRequest">The type of the request.</typeparam>
        /// <typeparam name="TResponse">The type of the response.</typeparam>
        /// <returns>The entry point of the execution of the request pipeline.</returns>
        RequestHandlerDelegate<TResponse> BuildPipeline<TRequest, TResponse>(TRequest request, IServiceProvider serviceProvider, RequestHandlerDelegate<TResponse> handler)
            where TRequest : IRequest<TResponse>;
    }
}