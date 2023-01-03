using FastEndpoints;
using Microsoft.AspNetCore.Http;
using System.Linq.Expressions;
using Vitastic.Domain.Data;
using Vitastic.Domain.Data.Models;
using Vitastic.Domain.Data.Queries;
using VitasticCore.SharedKernal.Functional;
using VitasticCore.SharedKernal.Responses.Collections;

namespace Vitastic.Domain.Endpoints.Recipes;
public class ListRecipes : Endpoint<ListRecipesRequest, IResult<IItemSet<ListRecipesResponse>>>
{
    private readonly IFoodStuffsData _data;

    public ListRecipes(IFoodStuffsData data)
    {
        _data = data;
    }

    public override void Configure()
    {
        Get("/recipes");
        Description(b => b
         //.Produces<IItemSet<IFailure>>(400, "application/json")
         .Produces<IItemSet<ListRecipesResponse>>(200));

        PreProcessors(new ListRecipesRequestLogger());
        PostProcessors(new IItemSetResponseLogger<ListRecipesRequest, IResult<IItemSet<ListRecipesResponse>>, ListRecipesResponse>());
    
    }

    public override async Task HandleAsync(ListRecipesRequest request, CancellationToken cancellationToken)
    {

        var paginationOptions = request.GetPaginationOptions();

        var searchCriteria = GetSearchCriteria(request);

        var allSearch = new RecipesSearchSpecification(searchCriteria);

        var totalCount = await _data.Recipes.Count(allSearch, cancellationToken).ConfigureAwait(false);

        var pagedSearch = new RecipesSearchSpecification(
            criteria: searchCriteria,
            paginationOptions: paginationOptions,
            sortBy: request.SortBy,
            sortDesc: request.SortDesc);

        var recipes = await _data.Recipes.List(pagedSearch, cancellationToken).ConfigureAwait(false);

        var response = recipes
            .Select(r => new ListRecipesResponse()
            {
                Id = r.Id,
                Name = r.Name,
                Categories = r.CategoryRecipes.Select(cr => cr.Category.Name).OrderBy(n => n),
                ImageId = r.PinnedImageId ?? r.Images.FirstOrDefault()?.Id
            })
            .ToItemSet(paginationOptions, totalCount)
            .Map(Result.Ok);

        await SendAsync(response);
    }

    private static Expression<Func<Recipe, bool>>[] GetSearchCriteria(ListRecipesRequest request)
    {
        var searchCriteria = new List<Expression<Func<Recipe, bool>>>();

        // StringComparison overloads aren't supported in EF's SQL Server driver, but we want to ensure case-insensitive compare regardless of collation
#pragma warning disable RCS1155

        if (!string.IsNullOrWhiteSpace(request.NameSearch))
        {
            searchCriteria.Add(recipe => recipe.Name.ToLower().Contains(request.NameSearch.ToLower()));
        }

        if (!string.IsNullOrWhiteSpace(request.CategorySearch))
        {
            searchCriteria.Add(recipe => recipe.CategoryRecipes.Any(cr => cr.Category.Name.ToLower().Contains(request.CategorySearch.ToLower())));
        }

#pragma warning restore RCS1155

        return searchCriteria.ToArray();
    }
}
