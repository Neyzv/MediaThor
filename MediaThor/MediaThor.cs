using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MediaThor
{
    internal sealed class MediaThor
        : IMediator
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IMediaThorHandlerProvider _handlerProvider;
        private readonly IPipelineBuilder _pipelineBuilder;

        public MediaThor(IServiceProvider serviceProvider,
            IMediaThorHandlerProvider handlerProvider,
            IPipelineBuilder pipelineBuilder)
        {
            _serviceProvider = serviceProvider;
            _handlerProvider = handlerProvider;
            _pipelineBuilder = pipelineBuilder;
        }

        /// <inheritdoc/>
        public Task Send<TRequest>(TRequest request, CancellationToken cancellationToken = default)
            where TRequest : IRequest =>
            _pipelineBuilder.BuildPipeline(request, _serviceProvider, _handlerProvider.TryGetHandler(_serviceProvider, request)).Invoke(cancellationToken);
        
        /// <inheritdoc/>
        public Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default) =>
            _pipelineBuilder.BuildPipeline(request, _serviceProvider, _handlerProvider.TryGetHandler(_serviceProvider, request)).Invoke(cancellationToken);

        /// <inheritdoc/>
        public Task<IAsyncEnumerable<TResponse>> CreateStream<TResponse>(IStreamRequest<TResponse> request,
            CancellationToken cancellationToken = default)
        {
            return _pipelineBuilder.BuildPipeline(request, _serviceProvider, _handlerProvider.TryGetHandler(_serviceProvider, request))
                .Invoke(cancellationToken);
        }
    }
}

