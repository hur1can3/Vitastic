using FastEndpoints;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using System.Globalization;
using Vitastic.Domain.Data;
using Vitastic.Domain.Data.Models;
using Vitastic.Domain.Data.Queries;
using VitasticCore.SharedKernal.Functional;
using VitasticCore.SharedKernal.Responses.Collections;
using VitasticCore.SharedKernal.Responses.Messages;

namespace Vitastic.Domain.Endpoints.Recipes;

public class SaveRecipe : Endpoint<SaveRecipeRequest, IResult<EntityMessage<int>>, SaveRecipeMapper>
{
    private readonly IFoodStuffsData _data;

    public SaveRecipe(IFoodStuffsData data)
    {
        _data = data;
    }


    public override void Configure()
    {
        Post("/recipes");
        Description(b => b
                //.Produces<IItemSet<IFailure>>(400, "application/json")
                .Produces<EntityMessage<int>>(200));

        PreProcessors(new SaveRecipeRequestLogger());
        PostProcessors(new EntityMessageResponseLogger<SaveRecipeRequest, IResult<EntityMessage<int>>>());
      
    }

    public override async Task HandleAsync(SaveRecipeRequest request, CancellationToken cancellationToken)
    {
        var byId = new RecipesByIdWithCategoriesAndImagesSpecification(request.Id);

        var maybeRecipe = await _data.Recipes.Get(byId, cancellationToken)
            .ConfigureAwait(false);

        if (maybeRecipe.HasValue)
        {
            var responseUpdated = await maybeRecipe.Value
                .TeeAsync(r => _data.Recipes.Update(r, cancellationToken))
                .TeeAsync(r => ManageCategories(request, r, cancellationToken))
                .MapAsync(r => Result.Ok(Map.FromEntity(r)))
                .ConfigureAwait(false);
            await SendAsync(responseUpdated);

        }

        var responseCreated = await Map.ToEntityAsync(request, cancellationToken)
            .TeeAsync(r => _data.Recipes.Add(r, cancellationToken))
            .TeeAsync(r => ManageCategories(request, r, cancellationToken))
            .MapAsync(r => Result.Ok(Map.FromEntity(r)))
            .ConfigureAwait(false);

        await SendAsync(responseCreated);
    }


    private async Task ManageCategories(SaveRecipeRequest request, Recipe recipe, CancellationToken cancellationToken)
    {
        var requested = request.Categories
            .Where(n => !string.IsNullOrWhiteSpace(n))
            .Select(n => n.ToLower().Trim())
            .ToArray();

        var categoriesThatMatchRequestedSpec = new CategoriesSpecification(
            c => requested.Contains(c.Name.ToLower().Trim()));

        var categoriesExist = (await _data.Categories
            .List(categoriesThatMatchRequestedSpec, cancellationToken)
            .ConfigureAwait(false))
            .Select(c => c.Name.ToLower().Trim());

        // Add categories that don't exist
        _ = await requested
            .Where(n => !categoriesExist.Contains(n))
            .Select(n => new Category { Name = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(n) })
            .TeeAsync(r => _data.Categories.AddRange(r, cancellationToken))
            .ConfigureAwait(false);

        // Remove relations that are no longer needed
        _ = await recipe.CategoryRecipes
            .Where(r => !requested.Contains(r.Category.Name.ToLower().Trim()))
            .TeeAsync(r => _data.CategoryRecipes.RemoveRange(r, cancellationToken))
            .ConfigureAwait(false);

        // Add relations that don't exist
        _ = await _data.Categories
            .List(categoriesThatMatchRequestedSpec, cancellationToken)
            .MapAsync(categories => categories
               .Where(c => !recipe.CategoryRecipes
                  .Select(r => r.Category.Name.ToLower().Trim())
                  .Contains(c.Name.ToLower().Trim()))
               .Select(c => new CategoryRecipe
               {
                   RecipeId = recipe.Id,
                   CategoryId = c.Id
               }))
            .TeeAsync(r => _data.CategoryRecipes.AddRange(r, cancellationToken))
            .ConfigureAwait(false);
    }


}

