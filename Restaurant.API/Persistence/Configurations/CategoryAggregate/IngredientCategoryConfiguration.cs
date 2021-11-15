namespace Restaurant.API.Persistence.Configurations.CategoryAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant.API.Entitites.IngredientCategoryEntities;

internal class IngredientCategoryConfiguration : IEntityTypeConfiguration<IngredientCategory>
{
    public void Configure(EntityTypeBuilder<IngredientCategory> entity)
    {
        entity.Property(c => c.Name)
              .IsRequired()
              .HasMaxLength(40);

        entity.Property(c => c.ImageUrl)
              .IsRequired()
              .HasMaxLength(100)
              .IsUnicode(false);

        entity.HasIndex(s => s.Name)
              .IsUnique();
    }
}
