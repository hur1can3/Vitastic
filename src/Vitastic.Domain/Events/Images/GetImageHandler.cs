using Vitastic.Domain.Data;
using Vitastic.Domain.Data.Queries;
using System.Threading;
using System.Threading.Tasks;
using VitasticCore.SharedKernal.Events;
using VitasticCore.SharedKernal.Functional;
using VitasticCore.SharedKernal.Responses.Files;

namespace Vitastic.Domain.Events.Images;

public class GetImageHandler : EventHandlerAbstract<GetImageRequest, SimpleFile>
{
    private readonly IFoodStuffsData _data;

    public GetImageHandler(IFoodStuffsData data)
    {
        _data = data;
    }

    public override Task<IResult<SimpleFile>> Handle(GetImageRequest request, CancellationToken cancellationToken = default)
    {
        var byId = new ImagesByIdWithBlobsSpecification(request.Id);

        return _data.Images.Get(byId, cancellationToken)
            .ToResultAsync(new ImageNotFoundFailure())
            .SelectAsync(r => new SimpleFile(r.Blob.Bytes, $"{r.Id}"));
    }
}
