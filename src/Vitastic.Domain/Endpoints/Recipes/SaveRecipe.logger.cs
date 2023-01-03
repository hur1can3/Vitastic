using FastEndpoints;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Vitastic.Domain.Endpoints.Recipes;

public class SaveRecipeRequestLogger : IPreProcessor<SaveRecipeRequest>
{
    public Task PreProcessAsync(SaveRecipeRequest req, HttpContext ctx, List<ValidationFailure> failures, CancellationToken ct)
    {
        var logger = ctx.Resolve<ILogger<SaveRecipeRequest>>();

        logger.LogInformation($"Requested:{req?.GetType().FullName} path: {ctx.Request.Path}. RecipeId: {req?.Id}");

        return Task.CompletedTask;
    }
}

