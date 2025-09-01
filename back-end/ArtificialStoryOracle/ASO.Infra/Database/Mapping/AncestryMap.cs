using ASO.Domain.Game.Entities;
using ASO.Infra.Database.Seeds;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ASO.Infra.Database.Mapping;

public class AncestryMap : IEntityTypeConfiguration<Ancestry>
{
    public void Configure(EntityTypeBuilder<Ancestry> builder)
    {
        builder.ToTable("ancestries");
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Name)
            .HasColumnName("name")
            .IsRequired()
            .HasMaxLength(50);
        
        builder.Property(a => a.Backstory)
            .HasColumnName("backstory")
            .IsRequired()
            .HasMaxLength(500);
        
        builder.Property(a => a.Size)
            .HasColumnName("size")
            .IsRequired();
        
        builder.Property(a => a.Displacement)
            .HasColumnName("displacement")
            .IsRequired();
        
        builder.ConfigureTracker();
    }
}