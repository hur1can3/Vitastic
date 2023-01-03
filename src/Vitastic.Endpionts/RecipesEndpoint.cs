using Microsoft.AspNetCore.Mvc;
using Vitastic.Domain.Events.Recipes;
using VitasticCore.AspNet.ClientApp;
using VitasticCore.AspNet.EndpointMapper;
using VitasticCore.SharedKernal.Functional;
using VitasticCore.SharedKernal.Responses.Collections;
using VitasticCore.SharedKernal.Responses.Messages;

namespace Vitastic.Endpionts;
public class RecipesEndpoint : IEndpoint
{

    /// <summary>
    /// Search for recipes using the following criteria. All are optional and some have defaults.
    /// </summary>
    /// <param name="listPipeline"></param>
    /// <param name="name">Name contains (case-insensitive)</param>
    /// <param name="category">Category names contain (case-insensitive)</param>
    /// <param name="sortBy">Field name to sort by (case-insensitive). Options are: name. Default is ID</param>
    /// <param name="sortDesc">True for descending sort</param>
    /// <param name="isPagingEnabled">False for all results</param>
    /// <param name="page">The page of results to retrieve</param>
    /// <param name="take">How many items in a page</param>
    /// <returns></returns>
    [HttpMapGet("/recipes")]
    [ProducesResponseType(typeof(IItemSet<ListRecipesResponse>), 200)]
    [ProducesResponseType(typeof(IItemSet<IFailure>), 400)]
    public async Task<Microsoft.AspNetCore.Http.IResult> Search([FromServices] ListRecipesPipeline listPipeline, string? name = null, string? category = null, string? sortBy = null, bool sortDesc = false, bool isPagingEnabled = true, int page = 1, int take = 30)
    {
        var request = new ListRecipesRequest(
            NameSearch: name,
            CategorySearch: category,
            SortBy: sortBy,
            SortDesc: sortDesc,
            IsPagingEnabled: isPagingEnabled,
            Page: page,
            Take: take);

        // Cancel long-running queries
        using var cts = new CancellationTokenSource()
            .Tee(c => c.CancelAfter(5000));

        return await listPipeline
            .Handle(request, cts.Token)
            .MapAsync(TypedHttpResponder.Respond);

    }

    /// <summary>
    /// Get a recipe.
    /// </summary>
    /// <param name="getPipeline"></param>
    /// <param name="id">The ID of the recipe to get</param>
    /// <returns></returns>
    //[Route("{id}")]
    //[HttpGet]
    [HttpMapGet("/recipes/{id}")]
    [ProducesResponseType(typeof(GetRecipeResponse), 200)]
    [ProducesResponseType(typeof(IItemSet<IFailure>), 400)]
    public Task<Microsoft.AspNetCore.Http.IResult> Get([FromServices] GetRecipePipeline getPipeline, int id)
    {
        var request = new GetRecipeRequest(id);

        return getPipeline
            .Handle(request)
            .MapAsync(TypedHttpResponder.Respond);

    }

    /// <summary>
    /// Save a recipe. Will update if found, otherwise a new recipe will be created.
    /// </summary>
    /// <param name="savePipeline"></param>
    /// <param name="request">The recipe to save</param>
    /// <returns></returns>
    //[HttpPost]
    [HttpMapPost("/recipes")]
    [ProducesResponseType(typeof(EntityMessage<int>), 200)]
    [ProducesResponseType(typeof(IItemSet<IFailure>), 400)]
    public Task<Microsoft.AspNetCore.Http.IResult> Save([FromServices] SaveRecipePipeline savePipeline, [FromBody] SaveRecipeRequest request)
    {
        return savePipeline
            .Handle(request)
            .MapAsync(TypedHttpResponder.Respond);

    }

    /// <summary>
    /// Delete a recipe.
    /// </summary>
    /// <param name="deletePipeline"></param>
    /// <param name="id">The ID of the recipe</param>
    /// <returns></returns>
    // [Route("{id}")]
    //[HttpDelete]
    [HttpMapDelete("/recipes/{id}")]
    [ProducesResponseType(typeof(EntityMessage<int>), 200)]
    [ProducesResponseType(typeof(IItemSet<IFailure>), 400)]
    public Task<Microsoft.AspNetCore.Http.IResult> Delete([FromServices] DeleteRecipePipeline deletePipeline, int id)
    {
        var request = new DeleteRecipeRequest(id);

        return deletePipeline
            .Handle(request)
            .MapAsync(TypedHttpResponder.Respond);
    }
}
