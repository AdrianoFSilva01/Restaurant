namespace Restaurant.API.Persistence.Configurations.CatalogAggregate;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant.API.Entitites.CategoryEntities;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> entity)
    {
        entity.Property(c => c.Name)
              .IsRequired()
              .HasMaxLength(40);

        entity.Property(c => c.Name)
              .IsRequired();

        entity.Property(c => c.ImageUrl)
              .IsRequired()
              .HasMaxLength(300)
              .IsUnicode(false);

        entity.Property(c => c.HeroImageUrl)
              .IsRequired()
              .HasMaxLength(300)
              .IsUnicode(false);

        entity.HasIndex("Name")
              .IsUnique();

        entity.HasMany(sc => sc.Catalogs)
              .WithOne(c => c.Category)
              .IsRequired()
              .OnDelete(DeleteBehavior.Restrict);
    }
}
