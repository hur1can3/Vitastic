using FastEndpoints;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vitastic.Domain.Endpoints.Recipes;
public class ListRecipesRequestLogger : IPreProcessor<ListRecipesRequest>
{
    public Task PreProcessAsync(ListRecipesRequest req, HttpContext ctx, List<ValidationFailure> failures, CancellationToken ct)
    {
        var logger = ctx.Resolve<ILogger<ListRecipesRequest>>();

        logger.LogInformation($"Requested:{req?.GetType().FullName} path: {ctx.Request.Path}. NameSearch: {req?.NameSearch} RequestCategorySearch: {req?.CategorySearch} RequestSort: {req?.SortBy} RequestIsPagingEnabled: {req?.IsPagingEnabled} RequestPage: {req?.Page} RequestTake: {req?.Take}");
        return Task.CompletedTask;
    }
}
