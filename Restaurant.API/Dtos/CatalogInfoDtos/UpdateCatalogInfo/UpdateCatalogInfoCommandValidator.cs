namespace Restaurant.API.Dtos.CatalogInfoDtos.UpdateCatalogInfo;
using FluentValidation;

public class UpdateCatalogInfoCommandValidator : AbstractValidator<UpdateCatalogInfoCommand>
{
    public UpdateCatalogInfoCommandValidator()
    {
        RuleFor(i => i.CatalogId).GreaterThan(0);
        RuleFor(n => n.Size).MaximumLength(25).NotEmpty();
        RuleFor(im => im.Price).GreaterThan(0);
    }
}
