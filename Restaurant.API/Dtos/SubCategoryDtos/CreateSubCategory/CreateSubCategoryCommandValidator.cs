namespace Restaurant.API.Dtos.SubCategoryDtos.CreateSubCategory;
using FluentValidation;

public class CreateSubCategoryCommandValidator : AbstractValidator<CreateSubCategoryCommand>
{
    public CreateSubCategoryCommandValidator()
    {
        RuleFor(x => x.Name).MaximumLength(25).NotEmpty();
        RuleFor(x => x.ImageUrl).NotEmpty();
        RuleFor(x => x.CategoryId).GreaterThan(0);
    }
}
