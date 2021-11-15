namespace Restaurant.API.Persistence.Configurations.CatalogAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant.API.Entitites.CatalogEntities;

public class CatalogInfoConfiguration : IEntityTypeConfiguration<CatalogInfo>
{
    public void Configure(EntityTypeBuilder<CatalogInfo> entity)
    {
        entity.Property(s => s.Size)
              .IsRequired()
              .HasMaxLength(15);

        entity.Property(s => s.Price)
              .IsRequired();

        entity.Property(s => s.Description)
              .HasMaxLength(25);

        entity.Property<int>("CatalogId")
              .IsRequired();

        entity.HasKey("CatalogId", "Size");

        entity.HasOne(sc => sc.Catalog)
              .WithMany(c => c.CatalogInfos)
              .HasForeignKey("CatalogId")
              .IsRequired()
              .OnDelete(DeleteBehavior.Restrict);
    }
}
