using FastEndpoints;
using Vitastic.Domain.Data.Models;
using VitasticCore.SharedKernal.Responses.Messages;

namespace Vitastic.Domain.Endpoints.Recipes;

public class SaveRecipeMapper : Mapper<SaveRecipeRequest, EntityMessage<int>, Recipe>
{
    public override Recipe ToEntity(SaveRecipeRequest r)
    {
        return new Recipe
        {
            Name = r.Name,
            Ingredients = r.Ingredients,
            Directions = r.Directions,
            CookTimeMinutes = r.CookTimeMinutes,
            PrepTimeMinutes = r.PrepTimeMinutes
        };
    }

    public override EntityMessage<int> FromEntity(Recipe e)
    {
        return EntityMessage.Create("Recipe Saved.", e.Id);
    }
}

