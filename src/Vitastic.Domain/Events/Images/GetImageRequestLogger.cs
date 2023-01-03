using Microsoft.Extensions.Logging;
using VitasticCore.SharedKernal.Events;

namespace Vitastic.Domain.Events.Images;

public class GetImageRequestLogger : RequestLoggerAbstract<GetImageRequest>
{
    public GetImageRequestLogger(ILogger<GetImageRequestLogger> logger) : base(logger) { }

    public override void Log(GetImageRequest request)
    {
        Logger.LogInformation("Requested. ImageId: {ImageId}",
            request.Id);
    }
}
