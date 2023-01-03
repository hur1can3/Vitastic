using FastEndpoints;
using FluentValidation;

namespace Vitastic.Domain.Endpoints.Recipes;

public class SaveRecipeRequestValidator : Validator<SaveRecipeRequest>
{
    public SaveRecipeRequestValidator()
    {

        _ = RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Please enter a name.")
            .MinimumLength(2)
            .WithMessage("Your name is too short!");

        _ = RuleFor(x => x.Ingredients)
            .NotEmpty()
            .WithMessage("Please enter ingredients.");

        _ = RuleFor(x => x.Directions)
            .NotEmpty()
            .WithMessage("Please enter directions.");

        _ = RuleFor(x => x.CookTimeMinutes)
            .GreaterThan(0)
            .WithMessage("Cook time must be positive.");

        _ = RuleFor(x => x.PrepTimeMinutes)
            .GreaterThan(0)
            .WithMessage("Prep time must be positive.");
    }
}

