namespace Restaurant.API.Dtos.IngredientCategoryDtos.UpdateIngredientCategory;
using FluentValidation;

public class UpdateIngredientCategoryCommandValidator : AbstractValidator<UpdateIngredientCategoryCommand>
{
    public UpdateIngredientCategoryCommandValidator()
    {
        RuleFor(n => n.Name).MaximumLength(25).NotEmpty();
        RuleFor(im => im.ImageUrl).NotEmpty();
    }
}
