using Microsoft.Extensions.Logging;
using VitasticCore.SharedKernal.Responses.Messages;

namespace Vitastic.Domain.Events.Images;

public class PinImageResponseLogger : EntityMessageEventLogger<PinImageRequest, int>
{
    public PinImageResponseLogger(ILogger<PinImageResponseLogger> logger) : base(logger) { }
}
