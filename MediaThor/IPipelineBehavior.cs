using System.Threading;
using System.Threading.Tasks;

namespace MediaThor
{
    /// <summary>
    /// Delegate to propagate the request through the next pipeline step.
    /// </summary>
    /// <typeparam name="TResponse">The type of the response.</typeparam>
    public delegate Task<TResponse> RequestHandlerDelegate<TResponse>(CancellationToken t = default);

    /// <summary>
    /// An interface that should be implemented to apply a specific behavior on a request that satisfy the generic types constraints.
    /// </summary>
    /// <typeparam name="TRequest">The type of the request.</typeparam>
    /// <typeparam name="TResponse">The type of the response.</typeparam>
    public interface IPipelineBehavior<in TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        /// <summary>
        /// Apply a specific behavior.
        /// </summary>
        /// <param name="request">The request that needs to be treated.</param>
        /// <param name="next">The next step of the pipeline to retrieve the result.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The pipeline execution result.</returns>
        Task<TResponse> HandleAsync(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken);
    }
    
    /// <summary>
    /// Delegate to propagate the request through the next pipeline step.
    /// </summary>
    public delegate Task RequestHandlerDelegate(CancellationToken t = default);

    /// <summary>
    /// An interface that should be implemented to apply a specific behavior on a request that satisfy the generic type constraint. 
    /// </summary>
    /// <typeparam name="TRequest">The type of the request.</typeparam>
    public interface IPipelineBehavior<in TRequest>
        where TRequest : IRequest
    {
        /// <summary>
        /// Apply a specific behavior.
        /// </summary>
        /// <param name="request">The request that needs to be treated.</param>
        /// <param name="next">The next step of the pipeline.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task HandleAsync(TRequest request, RequestHandlerDelegate next, CancellationToken cancellationToken);
    }
}

