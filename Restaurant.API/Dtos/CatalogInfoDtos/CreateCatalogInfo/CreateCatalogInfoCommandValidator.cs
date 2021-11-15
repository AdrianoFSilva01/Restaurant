namespace Restaurant.API.Dtos.CatalogInfoDtos.CreateCatalogInfo;
using FluentValidation;

public class CreateCatalogInfoCommandValidator : AbstractValidator<CreateCatalogInfoCommand>
{
    public CreateCatalogInfoCommandValidator()
    {
        RuleFor(x => x.Size).MaximumLength(15).NotEmpty();
        RuleFor(x => x.Price).GreaterThan(0);
        RuleFor(x => x.Description).MaximumLength(25);
        RuleFor(x => x.CatalogId).GreaterThan(0);
    }
}
