namespace Restaurant.API.Dtos.IngredientDtos.UpdateIngredient;
using FluentValidation;

public class UpdateIngredientCommandValidator : AbstractValidator<UpdateIngredientCommand>
{
    public UpdateIngredientCommandValidator()
    {
        RuleFor(n => n.Name).MaximumLength(25).NotEmpty();
        RuleFor(im => im.ImageUrl).NotEmpty();
        RuleFor(s => s.SubCategoryId).GreaterThan(0);
    }
}
