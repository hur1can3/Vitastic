using Vitastic.Domain.Data.Models;
using VitasticCore.SharedKernal.Data;

namespace Vitastic.Domain.Data;

/// <summary>
/// Represents all the tables, views and functions of the database.
/// </summary>
public interface IFoodStuffsData
{
    IWritableRepository<Blob> Blobs { get; }
    IWritableRepository<Category> Categories { get; }
    IWritableRepository<CategoryRecipe> CategoryRecipes { get; }
    IWritableRepository<Image> Images { get; }
    IWritableRepository<Recipe> Recipes { get; }
    IWritableRepository<User> Users { get; }
}
