namespace Restaurant.API.Dtos.SubCategoryDtos.CreateSubCategory;
public class CreateSubCategoryCommand
{
    public string Name { get; set; }
    public string ImageUrl { get; set; }
    public int CategoryId { get; set; }
}
