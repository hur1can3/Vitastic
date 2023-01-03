using Vitastic.Domain.Data;
using Vitastic.Domain.Data.Models;
using Vitastic.Domain.Data.Queries;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using VitasticCore.SharedKernal.Events;
using VitasticCore.SharedKernal.Functional;
using VitasticCore.SharedKernal.Responses.Messages;

namespace Vitastic.Domain.Events.Recipes;

public class DeleteRecipeHandler : EventHandlerAbstract<DeleteRecipeRequest, EntityMessage<int>>
{
    private readonly IFoodStuffsData _data;

    public DeleteRecipeHandler(IFoodStuffsData data)
    {
        _data = data;
    }

    public override Task<IResult<EntityMessage<int>>> Handle(DeleteRecipeRequest request, CancellationToken cancellationToken = default)
    {
        var byId = new RecipesByIdWithCategoriesAndImagesSpecification(request.Id);

        return _data.Recipes.Get(byId, cancellationToken)
            .ToResultAsync(new RecipeNotFoundFailure())
            .TeeOnSuccessAsync(r => RemoveImages(r, cancellationToken))
            .TeeOnSuccessAsync(r => _data.CategoryRecipes.RemoveRange(r.CategoryRecipes, cancellationToken))
            .TeeOnSuccessAsync(r => _data.Recipes.Remove(r, cancellationToken))
            .SelectAsync(r => EntityMessage.Create("Recipe deleted.", r.Id));
    }

    private async Task RemoveImages(Recipe recipe, CancellationToken cancellationToken)
    {
        var images = recipe.Images;
        // Optimization: don't bring the whole blob into RAM.
        var blobs = images.Select(i => new Blob { Id = i.Id });

        await _data.Blobs.RemoveRange(blobs, cancellationToken).ConfigureAwait(false);
        await _data.Images.RemoveRange(images, cancellationToken).ConfigureAwait(false);
    }
}
