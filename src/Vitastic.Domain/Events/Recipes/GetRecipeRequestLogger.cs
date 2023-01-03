using Microsoft.Extensions.Logging;
using VitasticCore.SharedKernal.Events;

namespace Vitastic.Domain.Events.Recipes;

public class GetRecipeRequestLogger : RequestLoggerAbstract<GetRecipeRequest>
{
    public GetRecipeRequestLogger(ILogger<GetRecipeRequestLogger> logger) : base(logger) { }

    public override void Log(GetRecipeRequest request)
    {
        Logger.LogInformation("Requested. RecipeId: {RecipeId}",
            request.Id);
    }
}
