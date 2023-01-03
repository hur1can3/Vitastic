using System.Collections.Generic;

#nullable disable

namespace Vitastic.Domain.Data.Models;

public partial class Category
{
    public Category()
    {
        CategoryRecipes = new HashSet<CategoryRecipe>();
    }

    public int Id { get; set; }
    public string Name { get; set; }

    public virtual ICollection<CategoryRecipe> CategoryRecipes { get; set; }
}
