using VitasticCore.SharedKernal.Events;
using VitasticCore.SharedKernal.Responses.Messages;

namespace Vitastic.Domain.Events.Images;

public class SaveImagePipeline : EventPipelineAbstract<SaveImageRequest, EntityMessage<int>>
{
    public SaveImagePipeline(SaveImageHandler handler, SaveImageRequestLogger requestLogger, SaveImageResponseLogger responseLogger)
    {
        InnerHandler = handler
            .AddRequestLogger(requestLogger)
            .AddPostProcessor(responseLogger);
    }
}
