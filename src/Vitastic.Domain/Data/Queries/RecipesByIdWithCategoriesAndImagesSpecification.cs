using Vitastic.Domain.Data.Models;
using VitasticCore.SharedKernal.Data;

namespace Vitastic.Domain.Data.Queries;

public class RecipesByIdWithCategoriesAndImagesSpecification : QuerySpecificationAbstract<Recipe>
{
    public RecipesByIdWithCategoriesAndImagesSpecification(int id) : base()
    {
        AddCriteria(r => r.Id == id);
        AddInclude($"{nameof(Recipe.CategoryRecipes)}.{nameof(CategoryRecipe.Category)}");
        AddInclude(nameof(Recipe.Images));
    }
}
