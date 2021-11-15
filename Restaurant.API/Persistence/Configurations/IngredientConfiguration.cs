namespace Restaurant.API.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant.API.Entitites;

internal class IngredientConfiguration : IEntityTypeConfiguration<Ingredient>
{
    public void Configure(EntityTypeBuilder<Ingredient> entity)
    {
        entity.Property(i => i.Name)
              .IsRequired()
              .HasMaxLength(40);

        entity.Property(i => i.ImageUrl)
              .IsRequired()
              .HasMaxLength(100)
              .IsUnicode(false);

        entity.Property(i => i.IngredientSubCategoryId)
              .IsRequired();

        entity.HasIndex(s => s.Name)
              .IsUnique();

        entity.HasOne(sc => sc.IngredientSubCategory)
              .WithMany(c => c.Ingredients)
              .HasForeignKey(x => x.IngredientSubCategoryId)
              .IsRequired()
              .OnDelete(DeleteBehavior.Restrict);
    }
}
