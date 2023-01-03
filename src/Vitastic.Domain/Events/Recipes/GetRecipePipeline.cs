using VitasticCore.SharedKernal.Events;

namespace Vitastic.Domain.Events.Recipes;

public class GetRecipePipeline : EventPipelineAbstract<GetRecipeRequest, GetRecipeResponse>
{
    public GetRecipePipeline(GetRecipeHandler handler, GetRecipeRequestLogger requestLogger, GetRecipeResponseLogger responseLogger)
    {
        InnerHandler = handler
            .AddRequestLogger(requestLogger)
            .AddPostProcessor(responseLogger);
    }
}
