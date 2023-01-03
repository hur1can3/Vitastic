namespace Vitastic.Domain.Events.Images;

public record SaveImageRequest(int RecipeId, byte[] FileContent);
