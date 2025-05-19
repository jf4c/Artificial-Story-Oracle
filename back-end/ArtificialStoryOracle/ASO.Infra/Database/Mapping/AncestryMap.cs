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
        
        builder.OwnsOne(a => a.Modifiers, modifiers =>
        {
            modifiers.Property(m => m.ModStrength)
                .HasColumnName("mod_strength")
                .IsRequired();
            
            modifiers.Property(m => m.ModDexterity)
                .HasColumnName("mod_dexterity")
                .IsRequired();
            
            modifiers.Property(m => m.ModConstitution)
                .HasColumnName("mod_constitution")
                .IsRequired();
            
            modifiers.Property(m => m.ModIntelligence)
                .HasColumnName("mod_intelligence")
                .IsRequired();
            
            modifiers.Property(m => m.ModWisdom)
                .HasColumnName("mod_wisdom")
                .IsRequired();
            
            modifiers.Property(m => m.ModCharisma)
                .HasColumnName("mod_charisma")
                .IsRequired();
        });
    }
}