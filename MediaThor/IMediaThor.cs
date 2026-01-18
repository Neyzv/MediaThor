using System.Threading;
using System.Threading.Tasks;

namespace MediaThor
{
    public interface IMediaThor
    {
        /// <summary>
        /// Send a request that needs to be handled, and retrieve its result from its appropriated Handler.
        /// </summary>
        /// <param name="request">The request to send.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <typeparam name="TResponse">The type of the response retrieved from the request.</typeparam>
        /// <returns>A task that contains the handler result.</returns>
        Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default);
    }
}

