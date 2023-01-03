using FastEndpoints;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VitasticCore.SharedKernal.Functional;
using VitasticCore.SharedKernal.Responses.Collections;
using VitasticCore.SharedKernal.Responses.Messages;

namespace Vitastic.Domain.Endpoints;
public class IItemSetResponseLogger<TRequest, TResponse, TEntity> : IPostProcessor<TRequest, TResponse>
{


    public Task PostProcessAsync(TRequest req, TResponse res, HttpContext ctx, IReadOnlyCollection<ValidationFailure> failures, CancellationToken ct)
    {
        var reqLogger = ctx.Resolve<ILogger<TResponse>>();

        if (res is IResult<IItemSet<TEntity>> response)
        {
            var resLogger = ctx.Resolve<ILogger<TResponse>>();

            if (response.IsSuccess)
            {
                resLogger.LogInformation("Responded with ItemSet. Count: {Count} IsPagingEnabled: {IsPagingEnabled} Page: {Page} Take: {Take} TotalCount: {TotalCount}",
                response.Value.Count,
                response.Value.IsPagingEnabled,
                response.Value.Page,
                response.Value.Take,
                response.Value.TotalCount
        );
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
