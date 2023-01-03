using Microsoft.Extensions.Logging;
using VitasticCore.SharedKernal.Events;

namespace Vitastic.Domain.Events.Images;

public class SaveImageRequestLogger : RequestLoggerAbstract<SaveImageRequest>
{
    public SaveImageRequestLogger(ILogger<SaveImageRequestLogger> logger) : base(logger) { }

    public override void Log(SaveImageRequest request)
    {
        Logger.LogInformation("Requested. RecipeId: {RecipeId} FileSize: {FileSize}",
            request.RecipeId,
            request.FileContent.Length);
    }
}
