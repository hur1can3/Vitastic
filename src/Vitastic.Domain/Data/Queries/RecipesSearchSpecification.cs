using System.Linq.Expressions;
using Vitastic.Domain.Data.Models;
using VitasticCore.SharedKernal.Data;
using VitasticCore.SharedKernal.Responses.Collections;

namespace Vitastic.Domain.Data.Queries;

public class RecipesSearchSpecification : QuerySpecificationAbstract<Recipe>
{
    public RecipesSearchSpecification(Expression<Func<Recipe, bool>>[] criteria) : base(criteria)
    {
        AddInclude($"{nameof(Recipe.CategoryRecipes)}.{nameof(CategoryRecipe.Category)}");
        AddInclude(nameof(Recipe.Images));
    }

    public RecipesSearchSpecification(Expression<Func<Recipe, bool>>[] criteria, PaginationOptions paginationOptions, string? sortBy = null, bool sortDesc = false) : this(criteria)
    {
        ApplyPaging(paginationOptions);

        switch (sortBy?.ToUpperInvariant())
        {
            case "NAME":
                AddOrderBy(recipe => recipe.Name, sortDesc);
                AddOrderBy(recipe => recipe.Id);
                break;

            default:
                AddOrderBy(recipe => recipe.Id, true);
                break;
        }
    }
}
