namespace Restaurant.API.Dtos.CatalogDtos.UpdateCatalog;
using FluentValidation;

public class UpdateCatalogCommandValidator : AbstractValidator<UpdateCatalogCommand>
{
    public UpdateCatalogCommandValidator()
    {
        RuleFor(i => i.Id).GreaterThan(0);
        RuleFor(n => n.Name).MaximumLength(25).NotEmpty();
        RuleFor(im => im.ImageUrl).NotEmpty();
    }
}
