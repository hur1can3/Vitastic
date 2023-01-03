using VitasticCore.SharedKernal.Events;
using VitasticCore.SharedKernal.Responses.Messages;

namespace Vitastic.Domain.Events.Images;

public class DeleteImagePipeline : EventPipelineAbstract<DeleteImageRequest, EntityMessage<int>>
{
    public DeleteImagePipeline(DeleteImageHandler handler, DeleteImageRequestLogger requestLogger, DeleteImageResponseLogger responseLogger)
    {
        InnerHandler = handler
            .AddRequestLogger(requestLogger)
            .AddPostProcessor(responseLogger);
    }
}
