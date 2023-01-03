using System.Collections.Generic;

namespace Vitastic.Domain.Events.Recipes;

public record ListRecipesResponse(
    int Id,
    string Name,
    IEnumerable<string> Categories,
    int? ImageId);
