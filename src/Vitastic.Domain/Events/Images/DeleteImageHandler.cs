using Vitastic.Domain.Data;
using Vitastic.Domain.Data.Models;
using Vitastic.Domain.Data.Queries;
using VitasticCore.SharedKernal.Events;
using VitasticCore.SharedKernal.Functional;
using VitasticCore.SharedKernal.Responses.Messages;

namespace Vitastic.Domain.Events.Images;

public class DeleteImageHandler : EventHandlerAbstract<DeleteImageRequest, EntityMessage<int>>
{
    private readonly IFoodStuffsData _data;

    public DeleteImageHandler(IFoodStuffsData data)
    {
        _data = data;
    }

    public override Task<IResult<EntityMessage<int>>> Handle(DeleteImageRequest request, CancellationToken cancellationToken = default)
    {
        var byId = new ImagesByIdWithRecipesSpecification(request.Id);

        return _data.Images.Get(byId, cancellationToken)
            .ToResultAsync(new ImageNotFoundFailure())
            .TeeOnSuccessAsync(async i =>
            {
                if (i.Recipe.PinnedImageId == i.Id)
                {
                    i.Recipe.PinnedImageId = null;
                    await _data.Recipes.Update(i.Recipe, cancellationToken).ConfigureAwait(false);
                }
            })
            .TeeOnSuccessAsync(i => _data.Blobs.Remove(new Blob { Id = i.Id }, cancellationToken))
            .TeeOnSuccessAsync(i => _data.Images.Remove(i, cancellationToken))
            .SelectAsync(i => EntityMessage.Create("Image deleted.", i.Id));
    }
}
