using Microsoft.Extensions.Logging;
using VitasticCore.SharedKernal.Responses.Messages;

namespace Vitastic.Domain.Events.Images;

public class DeleteImageResponseLogger : EntityMessageEventLogger<DeleteImageRequest, int>
{
    public DeleteImageResponseLogger(ILogger<DeleteImageResponseLogger> logger) : base(logger) { }
}
