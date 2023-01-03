using VitasticCore.SharedKernal.Events;
using VitasticCore.SharedKernal.Responses.Messages;

namespace Vitastic.Domain.Events.Recipes;

public class SaveRecipePipeline : EventPipelineAbstract<SaveRecipeRequest, EntityMessage<int>>
{
    public SaveRecipePipeline(SaveRecipeHandler handler, SaveRecipeRequestLogger requestLogger, SaveRecipeRequestValidator validator, SaveRecipeResponseLogger responseLogger)
    {
        InnerHandler = handler
            .AddRequestLogger(requestLogger)
            .AddRequestValidator(validator)
            .AddPostProcessor(responseLogger);
    }
}
