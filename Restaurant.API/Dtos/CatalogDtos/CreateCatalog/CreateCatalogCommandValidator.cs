namespace Restaurant.API.Dtos.CatalogDtos.CreateCatalog;
using FluentValidation;

public class CreateCatalogCommandValidator : AbstractValidator<CreateCatalogCommand>
{
    public CreateCatalogCommandValidator()
    {
        RuleFor(x => x.Name).MaximumLength(25).NotEmpty();
        RuleFor(x => x.ImageUrl).NotEmpty();
        RuleFor(x => x.CategoryId).GreaterThan(0);
    }
}
