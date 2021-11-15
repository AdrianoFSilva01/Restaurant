namespace Restaurant.API.Dtos.IngredientDtos.CreateIngredient;
using FluentValidation;

public class CreateIngredientCommandValidator : AbstractValidator<CreateIngredientCommand>
{
    public CreateIngredientCommandValidator()
    {
        RuleFor(x => x.Name).MaximumLength(25).NotEmpty();
        RuleFor(x => x.ImageUrl).NotEmpty();
        RuleFor(x => x.SubCategoryId).GreaterThan(0);
    }
}
