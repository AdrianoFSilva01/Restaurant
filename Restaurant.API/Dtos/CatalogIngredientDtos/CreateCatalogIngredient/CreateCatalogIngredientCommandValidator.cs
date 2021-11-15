namespace Restaurant.API.Dtos.CatalogIngredientDtos.CreateCatalogIngredient;
using FluentValidation;

public class CreateCatalogIngredientCommandValidator : AbstractValidator<CreateCatalogIngredientCommand>
{
    public CreateCatalogIngredientCommandValidator()
    {
        RuleFor(x => x.IngredientId).GreaterThan(0);
        RuleFor(x => x.CatalogId).GreaterThan(0);
    }
}
