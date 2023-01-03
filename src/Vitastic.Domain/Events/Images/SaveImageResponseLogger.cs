using Microsoft.Extensions.Logging;
using VitasticCore.SharedKernal.Responses.Messages;

namespace Vitastic.Domain.Events.Images;

public class SaveImageResponseLogger : EntityMessageEventLogger<SaveImageRequest, int>
{
    public SaveImageResponseLogger(ILogger<SaveImageResponseLogger> logger) : base(logger) { }
}
