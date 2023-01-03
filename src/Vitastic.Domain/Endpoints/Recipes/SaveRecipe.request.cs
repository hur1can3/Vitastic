namespace Vitastic.Domain.Endpoints.Recipes;

public class SaveRecipeRequest
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Ingredients { get; set; }
    public string? Directions { get; set; }
    public int? CookTimeMinutes { get; set; }
    public int? PrepTimeMinutes { get; set; }
    public IEnumerable<string>? Categories { get; set; }
}

