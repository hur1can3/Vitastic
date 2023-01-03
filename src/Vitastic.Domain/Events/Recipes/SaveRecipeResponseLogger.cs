using Microsoft.Extensions.Logging;
using VitasticCore.SharedKernal.Responses.Messages;

namespace Vitastic.Domain.Events.Recipes;

public class SaveRecipeResponseLogger : EntityMessageEventLogger<SaveRecipeRequest, int>
{
    public SaveRecipeResponseLogger(ILogger<SaveRecipeResponseLogger> logger) : base(logger) { }
}
