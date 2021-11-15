namespace Restaurant.API.Persistence;
using Microsoft.EntityFrameworkCore;
using Restaurant.API.Entitites;
using Restaurant.API.Entitites.CatalogEntities;
using Restaurant.API.Entitites.CategoryEntities;
using Restaurant.API.Entitites.IngredientCategoryEntities;

public class RestaurantDbContext : DbContext
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<Ingredient> Ingredients { get; set; }
    public DbSet<Catalog> Catalogs { get; set; }
    public DbSet<CatalogInfo> CatalogInfos { get; set; }
    public DbSet<IngredientCategory> IngredientCategories { get; set; }

    public RestaurantDbContext(DbContextOptions<RestaurantDbContext> options)
        : base(options)
    {
    }

    public RestaurantDbContext()
    {

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder
                .UseSqlServer("Server = (localdb)\\mssqllocaldb; Database = IngredientsTranslation; Trusted_Connection = True;");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(RestaurantDbContext).Assembly);
    }
}
