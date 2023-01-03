using Microsoft.Extensions.Logging;
using VitasticCore.SharedKernal.Events;

namespace Vitastic.Domain.Events.Recipes;

public class DeleteRecipeRequestLogger : RequestLoggerAbstract<DeleteRecipeRequest>
{
    public DeleteRecipeRequestLogger(ILogger<DeleteRecipeRequestLogger> logger) : base(logger) { }

    public override void Log(DeleteRecipeRequest request)
    {
        Logger.LogInformation("Requested. RecipeId: {RecipeId}",
            request.Id);
    }
}
