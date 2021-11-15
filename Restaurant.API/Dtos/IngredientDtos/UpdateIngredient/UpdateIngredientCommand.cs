namespace Restaurant.API.Dtos.IngredientDtos.UpdateIngredient;

public class UpdateIngredientCommand
{
    public string Name { get; private set; }
    public string ImageUrl { get; private set; }
    public int SubCategoryId { get; private set; }
}
