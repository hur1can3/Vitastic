using Vitastic.Domain.Data;
using Vitastic.Domain.Data.Queries;
using System.Threading;
using System.Threading.Tasks;
using VitasticCore.SharedKernal.Events;
using VitasticCore.SharedKernal.Functional;
using VitasticCore.SharedKernal.Responses.Messages;

namespace Vitastic.Domain.Events.Images;

public class PinImageHandler : EventHandlerAbstract<PinImageRequest, EntityMessage<int>>
{
    private readonly IFoodStuffsData _data;

    public PinImageHandler(IFoodStuffsData data)
    {
        _data = data;
    }

    public override Task<IResult<EntityMessage<int>>> Handle(PinImageRequest request, CancellationToken cancellationToken = default)
    {
        return _data.Images.Get(new ImagesByIdWithRecipesSpecification(request.Id), cancellationToken)
            .ToResultAsync(new ImageNotFoundFailure())
            .SelectAsync(i => i.Recipe)
            .TeeOnSuccessAsync(r => r.PinnedImageId = request.Id)
            .TeeOnSuccessAsync(r => _data.Recipes.Update(r, cancellationToken))
            .SelectAsync(_ => EntityMessage.Create("Image pinned.", request.Id));
    }
}
