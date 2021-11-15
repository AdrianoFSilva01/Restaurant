namespace Restaurant.API.Dtos.SubCategoryDtos.UpdateSubCategory;
using FluentValidation;

public class UpdateSubCategoryCommandValidator : AbstractValidator<UpdateSubCategoryCommand>
{
    public UpdateSubCategoryCommandValidator()
    {
        RuleFor(n => n.Name).MaximumLength(25).NotEmpty();
        RuleFor(im => im.ImageUrl).NotEmpty();
    }
}
