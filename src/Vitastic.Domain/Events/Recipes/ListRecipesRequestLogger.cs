using Microsoft.Extensions.Logging;
using VitasticCore.SharedKernal.Events;

namespace Vitastic.Domain.Events.Recipes;

public class ListRecipesRequestLogger : RequestLoggerAbstract<ListRecipesRequest>
{
    public ListRecipesRequestLogger(ILogger<ListRecipesRequestLogger> logger) : base(logger) { }

    public override void Log(ListRecipesRequest request)
    {
        Logger.LogInformation("Requested. NameSearch: {NameSearch} RequestCategorySearch: {CategorySearch} RequestSort: {SortBy} RequestIsPagingEnabled: {IsPagingEnabled} RequestPage: {Page} RequestTake: {Take}",
            request.NameSearch,
            request.CategorySearch,
            request.SortBy,
            request.IsPagingEnabled,
            request.Page,
            request.Take);
    }
}
