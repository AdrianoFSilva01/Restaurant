namespace Restaurant.API.Dtos.Queries.GetAll;
using System.Collections.Generic;

public class CategoryDetail
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string ImageUrl { get; set; }
    public List<SubCategoryDetail> SubCategories { get; set; }
}

public class SubCategoryDetail
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string ImageUrl { get; set; }
    public List<IngredientDetail> Ingredients { get; set; }
}

public class IngredientDetail
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string ImageUrl { get; set; }
}
