using VitasticCore.SharedKernal.Events;
using VitasticCore.SharedKernal.Responses.Messages;

namespace Vitastic.Domain.Events.Recipes;

public class DeleteRecipePipeline : EventPipelineAbstract<DeleteRecipeRequest, EntityMessage<int>>
{
    public DeleteRecipePipeline(DeleteRecipeHandler handler, DeleteRecipeRequestLogger requestLogger, DeleteRecipeResponseLogger responseLogger)
    {
        InnerHandler = handler
            .AddRequestLogger(requestLogger)
            .AddPostProcessor(responseLogger);
    }
}
