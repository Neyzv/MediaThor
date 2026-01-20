using System.Collections.Generic;

namespace MediaThor
{
    /// <summary>
    /// Represent a streamable request that needs to be handled.
    /// </summary>
    /// <typeparam name="TResponse">The streamable result type.</typeparam>
    public interface IStreamRequest<TResponse>
        : IRequest<IAsyncEnumerable<TResponse>> { }
}