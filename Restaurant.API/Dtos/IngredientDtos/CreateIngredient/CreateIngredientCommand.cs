namespace Restaurant.API.Dtos.IngredientDtos.CreateIngredient;

public class CreateIngredientCommand
{
    public string Name { get; set; }
    public string ImageUrl { get; set; }
    public int SubCategoryId { get; set; }
}
