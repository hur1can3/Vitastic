using VitasticCore.SharedKernal.Functional;
using VitasticCore.SharedKernal.Guards;

namespace VitasticCore.SharedKernal.Events;

/// <summary>
/// Construct the pipeline from the event components and store it to clean up controllers.
/// </summary>
/// <typeparam name="TRequest">The type of the event request</typeparam>
/// <typeparam name="TResponse">The type of the event response</typeparam>
public abstract class EventPipelineAbstract<TRequest, TResponse> : IEventHandler<TRequest, TResponse>
{
    /// <summary>
    /// Construct and store the InnerHandler as a complete event pipeline.
    /// </summary>
    protected IEventHandler<TRequest, TResponse>? InnerHandler;

    /// <inheritdoc/>
    public Task<IResult<TResponse>> Handle(TRequest request, CancellationToken cancellationToken = default)
    {
        return InnerHandler
            .EnsureNotNull()
            .Handle(request, cancellationToken);
    }
}
