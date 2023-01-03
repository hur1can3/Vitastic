using Microsoft.Extensions.Logging;
using VitasticCore.SharedKernal.Events;

namespace Vitastic.Domain.Events.Images;

public class PinImageRequestLogger : RequestLoggerAbstract<PinImageRequest>
{
    public PinImageRequestLogger(ILogger<PinImageRequestLogger> logger) : base(logger) { }

    public override void Log(PinImageRequest request)
    {
        Logger.LogInformation("Requested. ImageId: {ImageId}",
            request.Id);
    }
}
