using FastEndpoints;
using Microsoft.AspNetCore.Http;
using Vitastic.Domain.Data;
using Vitastic.Domain.Data.Models;
using Vitastic.Domain.Data.Queries;
using Vitastic.Domain.Events;
using VitasticCore.SharedKernal.Functional;
using VitasticCore.SharedKernal.Responses.Collections;
using VitasticCore.SharedKernal.Responses.Messages;

namespace Vitastic.Domain.Endpoints.Recipes;
public class DeleteRecipe : Endpoint<DeleteRecipeRequest, IResult<EntityMessage<int>>, DeleteRecipeMapper>
{
    private readonly IFoodStuffsData _data;

    public DeleteRecipe(IFoodStuffsData data)
    {
        _data = data;
    }


    public override void Configure()
    {
        Delete(routePattern: "/recipes/{@id}", x => new { x.Id });
        Description(b => b
        //.Produces<IItemSet<IFailure>>(400, "application/json")
        .Produces<EntityMessage<int>>(200));

        PreProcessors(new DeleteRecipeRequestLogger());
        PostProcessors(new EntityMessageResponseLogger<DeleteRecipeRequest, IResult<EntityMessage<int>>>());
    }

    public override async Task HandleAsync(DeleteRecipeRequest request, CancellationToken cancellationToken)
    {
        var byId = new RecipesByIdWithCategoriesAndImagesSpecification(request.Id);

        var response = await _data.Recipes.Get(byId, cancellationToken)
              .ToResultAsync(new RecipeNotFoundFailure())
              .TeeOnSuccessAsync(r => RemoveImages(r, cancellationToken))
              .TeeOnSuccessAsync(r => _data.CategoryRecipes.RemoveRange(r.CategoryRecipes, cancellationToken))
              .TeeOnSuccessAsync(r => _data.Recipes.Remove(r, cancellationToken))
              .SelectAsync(r => EntityMessage.Create("Recipe deleted.", r.Id))
              ;

        await SendAsync(response);
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
