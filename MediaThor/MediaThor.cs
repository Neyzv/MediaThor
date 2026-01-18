using System;
using System.Threading;
using System.Threading.Tasks;

namespace MediaThor
{
    public sealed class MediaThor
        : IMediaThor
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IMediaThorDispatcher _mediaThorDispatcher;

        public MediaThor(IServiceProvider serviceProvider, IMediaThorDispatcher mediaThorDispatcher)
        {
            _serviceProvider = serviceProvider;
            _mediaThorDispatcher = mediaThorDispatcher;
        }
        
        public Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default) =>
            _mediaThorDispatcher.Dispatch(_serviceProvider, request, cancellationToken);
    }
}

