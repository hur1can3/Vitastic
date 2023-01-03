using Microsoft.Extensions.Logging;
using VitasticCore.SharedKernal.Responses.Files;

namespace Vitastic.Domain.Events.Images;

public class GetImageResponseLogger : SimpleFileEventLogger<GetImageRequest>
{
    public GetImageResponseLogger(ILogger<GetImageResponseLogger> logger) : base(logger) { }
}
