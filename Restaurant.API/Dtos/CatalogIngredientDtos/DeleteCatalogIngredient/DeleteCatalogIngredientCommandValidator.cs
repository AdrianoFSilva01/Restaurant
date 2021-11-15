namespace Restaurant.API.Dtos.CatalogIngredientDtos.DeleteCatalogIngredient;
using FluentValidation;

public class DeleteCatalogIngredientCommandValidator : AbstractValidator<DeleteCatalogIngredientCommand>
{
    public DeleteCatalogIngredientCommandValidator()
    {
        RuleFor(i => i.CatalogId).GreaterThan(0);
        RuleFor(i => i.IngredientId).GreaterThan(0);
    }
}
