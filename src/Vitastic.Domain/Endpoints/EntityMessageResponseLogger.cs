using FastEndpoints;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using VitasticCore.SharedKernal.Functional;
using VitasticCore.SharedKernal.Responses.Messages;

namespace Vitastic.Domain.Endpoints;


public class EntityMessageResponseLogger<TRequest, TResponse> : IPostProcessor<TRequest, TResponse>
{

    public Task PostProcessAsync(TRequest req, TResponse res, HttpContext ctx, IReadOnlyCollection<ValidationFailure> failures, CancellationToken ct)
    {
        var reqLogger = ctx.Resolve<ILogger<TResponse>>();

        if (res is IResult<EntityMessage<int>> response)
        {
            var resLogger = ctx.Resolve<ILogger<TResponse>>();

            if (response.IsSuccess)
            {
                resLogger.LogInformation("Responded with {ResponseType}. id: {entityId}",
               typeof(TResponse).Name,
               response.Value.Id);
                resLogger.LogInformation(response.Value.Message);
            }

            if (response.IsFailed)
            {
                var failuresList = failures.ToList();

                resLogger.LogWarning("Count: {Count} Failures: {FailureMessages}",
                    failuresList.Count,
                    string.Join(" ", failuresList.Select(failure => failure.ErrorMessage))
                );
            }
        }
        return Task.CompletedTask;
    }
}
