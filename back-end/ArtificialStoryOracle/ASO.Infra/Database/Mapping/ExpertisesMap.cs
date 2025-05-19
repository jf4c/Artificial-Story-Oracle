using ASO.Domain.Game.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ASO.Infra.Database.Mapping;

public class ExpertisesMap : IEntityTypeConfiguration<Expertise>
{
    public void Configure(EntityTypeBuilder<Expertise> builder)
    {
        builder.ToTable("expertises");
        
        builder.HasKey(e => e.Id)
            .HasName("id");
        
        builder.Property(e => e.Name)
            .HasColumnName("name")
            .IsRequired()
            .HasMaxLength(50);
        
        builder.Property(e => e.KeyAttributes)
            .HasColumnName("key_attributes")
            .IsRequired();
        
        builder.Property(e => e.Trained)
            .HasColumnName("trained")
            .IsRequired();
        
        builder.Property(e => e.ArmorPenalty)
            .HasColumnName("armor_penalty")
            .IsRequired();
        
        builder.HasMany(e => e.Characters)
            .WithMany(c => c.Expertises)
            .UsingEntity(j => j.ToTable("character_expertises"));
    }
}