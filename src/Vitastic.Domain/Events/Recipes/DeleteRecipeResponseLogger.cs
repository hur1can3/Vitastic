using Microsoft.Extensions.Logging;
using VitasticCore.SharedKernal.Responses.Messages;

namespace Vitastic.Domain.Events.Recipes;

public class DeleteRecipeResponseLogger : EntityMessageEventLogger<DeleteRecipeRequest, int>
{
    public DeleteRecipeResponseLogger(ILogger<DeleteRecipeResponseLogger> logger) : base(logger) { }
}
