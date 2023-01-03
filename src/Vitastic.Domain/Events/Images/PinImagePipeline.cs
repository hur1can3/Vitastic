using VitasticCore.SharedKernal.Events;
using VitasticCore.SharedKernal.Responses.Messages;

namespace Vitastic.Domain.Events.Images;

public class PinImagePipeline : EventPipelineAbstract<PinImageRequest, EntityMessage<int>>
{
    public PinImagePipeline(PinImageHandler handler, PinImageRequestLogger requestLogger, PinImageResponseLogger responseLogger)
    {
        InnerHandler = handler
            .AddRequestLogger(requestLogger)
            .AddPostProcessor(responseLogger);
    }
}
