namespace Restaurant.API.Persistence.Configurations.CategoryAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant.API.Entitites.IngredientCategoryEntities;

internal class IngredientSubCategoryConfiguration : IEntityTypeConfiguration<IngredientSubCategory>
{
    public void Configure(EntityTypeBuilder<IngredientSubCategory> entity)
    {
        entity.Property(s => s.Name)
              .IsRequired()
              .HasMaxLength(40);

        entity.Property(s => s.ImageUrl)
              .IsRequired()
              .HasMaxLength(100)
              .IsUnicode(false);

        entity.HasIndex(s => s.Name)
              .IsUnique();

        entity.HasOne(sc => sc.IngredientCategory)
              .WithMany(c => c.IngredientSubCategories)
              .HasForeignKey("IngredientCategoryId")
              .IsRequired()
              .OnDelete(DeleteBehavior.Restrict);

        entity.HasMany(sc => sc.Ingredients)
              .WithOne(c => c.IngredientSubCategory)
              .HasForeignKey("IngredientSubCategoryId")
              .IsRequired()
              .OnDelete(DeleteBehavior.Restrict);
    }
}
