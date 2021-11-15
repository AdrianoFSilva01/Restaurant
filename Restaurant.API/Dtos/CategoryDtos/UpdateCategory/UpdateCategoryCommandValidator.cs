namespace Restaurant.API.Dtos.CategoryDtos.UpdateCategory;
using FluentValidation;

public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
{
    public UpdateCategoryCommandValidator()
    {
        RuleFor(i => i.Id).GreaterThan(0);
        RuleFor(n => n.Name).MaximumLength(25).NotEmpty();
        RuleFor(i => i.ImageUrl).NotEmpty();
    }
}
