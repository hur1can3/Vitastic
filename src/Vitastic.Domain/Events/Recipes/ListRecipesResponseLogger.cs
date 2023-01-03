using Microsoft.Extensions.Logging;
using VitasticCore.SharedKernal.Responses.Collections;

namespace Vitastic.Domain.Events.Recipes;

public class ListRecipesResponseLogger : ItemSetEventLogger<ListRecipesRequest, ListRecipesResponse>
{
    public ListRecipesResponseLogger(ILogger<ListRecipesResponseLogger> logger) : base(logger) { }
}
