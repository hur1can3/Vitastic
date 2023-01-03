
using VitasticCore.SharedKernal.Functional;

namespace Vitastic.Domain.Events;

public class RecipeNotFoundFailure : Failure
{
    public RecipeNotFoundFailure() : base(errorMessage: "Recipe not found.", uiHandle: "recipeId")
    {
    }
}
