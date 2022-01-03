namespace Restaurant.API.Persistence.Configurations.CatalogAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant.API.Entitites.CatalogEntities;

public class CatalogConfiguration : IEntityTypeConfiguration<Catalog>
{
    public void Configure(EntityTypeBuilder<Catalog> entity)
    {
        entity.Property(i => i.Name)
              .IsRequired()
              .HasMaxLength(40);

        entity.Property(i => i.ImageUrl)
              .IsRequired()
              .HasMaxLength(300)
              .IsUnicode(false);

        entity.Property(i => i.HeroImageUrl)
              .IsRequired(false)
              .HasMaxLength(300)
              .IsUnicode(false);

        entity.HasIndex("Name", "CategoryId")
              .IsUnique();

        entity.HasMany(i => i.Ingredients)
            .WithMany(i => i.Catalogs)
            .UsingEntity(i => i.ToTable("CatalogIngredient"));
    }
}
