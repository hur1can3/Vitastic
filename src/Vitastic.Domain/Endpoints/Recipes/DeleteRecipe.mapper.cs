using FastEndpoints;
using Vitastic.Domain.Data.Models;
using VitasticCore.SharedKernal.Responses.Messages;

namespace Vitastic.Domain.Endpoints.Recipes;

public class DeleteRecipeMapper : Mapper<DeleteRecipeRequest, EntityMessage<int>, Recipe>
{
    public override Recipe ToEntity(DeleteRecipeRequest r)
    {
        return new Recipe
        {
           Id = r.Id
        };
    }

    public override EntityMessage<int> FromEntity(Recipe e)
    {
        return EntityMessage.Create("Recipe Deleted.", e.Id);
    }
}

