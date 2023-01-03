using FastEndpoints;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Vitastic.Domain.Endpoints.Recipes;

public class DeleteRecipeRequestLogger : IPreProcessor<DeleteRecipeRequest>
{
    public Task PreProcessAsync(DeleteRecipeRequest req, HttpContext ctx, List<ValidationFailure> failures, CancellationToken ct)
    {
        var logger = ctx.Resolve<ILogger<DeleteRecipeRequest>>();

        logger.LogInformation($"Deleted:{req?.GetType().FullName} path: {ctx.Request.Path}. RecipeId: {req?.Id}");

        return Task.CompletedTask;
    }
}

