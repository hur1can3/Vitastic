using Microsoft.Extensions.Logging;
using VitasticCore.SharedKernal.Events;

namespace Vitastic.Domain.Events.Images;

public class DeleteImageRequestLogger : RequestLoggerAbstract<DeleteImageRequest>
{
    public DeleteImageRequestLogger(ILogger<DeleteImageRequestLogger> logger) : base(logger) { }

    public override void Log(DeleteImageRequest request)
    {
        Logger.LogInformation("Requested. ImageId: {ImageId}",
            request.Id);
    }
}
