namespace Restaurant.API.Dtos.IngredientCategoryDtos.CreateIngredientCategory;
using FluentValidation;

public class CreateIngredientCategoryCommandValidator : AbstractValidator<CreateIngredientCategoryCommand>
{
    public CreateIngredientCategoryCommandValidator()
    {
        RuleFor(x => x.Name).MaximumLength(25).NotEmpty();
        RuleFor(x => x.ImageUrl).NotEmpty();
    }
}
