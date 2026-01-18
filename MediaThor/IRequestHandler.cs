using System.Threading;
using System.Threading.Tasks;

namespace MediaThor
{
    /// <summary>
    /// An interface that should be implemented to handle a request and provide its result.
    /// </summary>
    /// <typeparam name="TRequest">The type of the request.</typeparam>
    /// <typeparam name="TResponse">The type of the response.</typeparam>
    public interface IRequestHandler<in TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        /// <summary>
        /// Provide a result for the associated <see cref="TRequest"/>.
        /// </summary>
        /// <param name="request">The instance of the request that should be treated.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The result of this request.</returns>
        Task<TResponse> HandleAsync(TRequest request, CancellationToken cancellationToken);
    }
}