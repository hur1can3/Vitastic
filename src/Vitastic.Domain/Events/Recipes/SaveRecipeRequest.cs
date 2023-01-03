using System.Collections.Generic;

namespace Vitastic.Domain.Events.Recipes;

public record SaveRecipeRequest(
    int Id,
    string Name,
    string Ingredients,
    string Directions,
    int? CookTimeMinutes,
    int? PrepTimeMinutes,
    IEnumerable<string> Categories);
