using ASO.Domain.Game.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ASO.Infra.Database.Mapping;

public class ImageMap : IEntityTypeConfiguration<Image>
{
    public void Configure(EntityTypeBuilder<Image> builder)
    {
        builder.ToTable("images");
        builder.HasKey(i => i.Id);
        builder.Property(i => i.Id)
            .HasColumnName("id");
        builder.Property(i => i.Name)
            .HasColumnName("name")
            .IsRequired();
        builder.Property(i => i.Description)
            .HasColumnName("description")
            .IsRequired(false);
        builder.Property(i => i.Url)
            .HasColumnName("url")
            .IsRequired();
    }
}