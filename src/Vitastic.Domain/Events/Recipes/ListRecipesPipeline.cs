using VitasticCore.SharedKernal.Events;
using VitasticCore.SharedKernal.Responses.Collections;

namespace Vitastic.Domain.Events.Recipes;

public class ListRecipesPipeline : EventPipelineAbstract<ListRecipesRequest, IItemSet<ListRecipesResponse>>
{
    public ListRecipesPipeline(ListRecipesHandler handler, ListRecipesRequestLogger requestLogger, ListRecipesResponseLogger responseLogger)
    {
        InnerHandler = handler
            .AddRequestLogger(requestLogger)
            .AddPostProcessor(responseLogger);
    }
}
