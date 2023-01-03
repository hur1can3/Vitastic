using VitasticCore.SharedKernal.Events;
using VitasticCore.SharedKernal.Responses.Files;

namespace Vitastic.Domain.Events.Images;

public class GetImagePipeline : EventPipelineAbstract<GetImageRequest, SimpleFile>
{
    public GetImagePipeline(GetImageHandler handler, GetImageRequestLogger requestLogger, GetImageResponseLogger responseLogger)
    {
        InnerHandler = handler
            .AddRequestLogger(requestLogger)
            .AddPostProcessor(responseLogger);
    }
}
