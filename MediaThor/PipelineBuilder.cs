using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace MediaThor
{
    internal sealed class PipelineBuilder
        : IPipelineBuilder
    {
        /// <inheritdoc/>
        public RequestHandlerDelegate BuildPipeline<TRequest>(TRequest request, IServiceProvider serviceProvider, RequestHandlerDelegate pipeline)
            where TRequest : IRequest
        {
            foreach (var behavior in serviceProvider.GetServices<IPipelineBehavior<TRequest>>().Reverse())
            {
                var next = pipeline;
                pipeline = ct => behavior.HandleAsync(request, next, ct);
            }

            return pipeline;
        }
        
        /// <inheritdoc/>
        public RequestHandlerDelegate<TResponse> BuildPipeline<TRequest, TResponse>(TRequest request, IServiceProvider serviceProvider, RequestHandlerDelegate<TResponse> pipeline)
            where TRequest : IRequest<TResponse>
        {
            var a = typeof(TRequest);
            foreach (var behavior in serviceProvider.GetServices<IPipelineBehavior<TRequest, TResponse>>().Reverse())
            {
                var next = pipeline;
                pipeline = ct => behavior.HandleAsync(request, next, ct);
            }

            return pipeline;
        }
    }
}