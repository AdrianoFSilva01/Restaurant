namespace Restaurant.API.Dtos.CategoryDtos.CreateCategory;
using FluentValidation;

public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
        RuleFor(x => x.Name).MaximumLength(25).NotEmpty();
        RuleFor(x => x.ImageUrl).NotEmpty();
    }
}
