namespace Restaurant.API.Dtos.CatalogDtos.CreateCatalog;
public class CreateCatalogCommand
{
    public string Name { get; set; }

    public string ImageUrl { get; set; }

    public int CategoryId { get; set; }
}
