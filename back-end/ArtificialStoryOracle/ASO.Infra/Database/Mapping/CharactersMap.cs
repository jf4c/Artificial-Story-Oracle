using ASO.Domain.Game.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ASO.Infra.Database.Mapping;

public class CharactersMap : IEntityTypeConfiguration<Character>
{
    public void Configure(EntityTypeBuilder<Character> builder)
    {
        builder.ToTable("characters");
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id)
            .HasColumnName("id");
        builder.OwnsOne(c => c.Name, name =>
        {
            name.Property(n => n.FirstName)
                .HasColumnName("first_name")
                .IsRequired();
            
            name.Property(n => n.LastName)
                .HasColumnName("last_name")
                .IsRequired();
        });
        
        builder.Property(c => c.TypeCharacter)
            .HasColumnName("type_character")
            .IsRequired();
        builder.Property(c => c.Level)
            .HasColumnName("level");
        
        builder.HasOne(c => c.Ancestry)
            .WithMany()
            .HasForeignKey("ancestry_id")
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(c => c.Expertises)
            .WithMany(c => c.Characters)
            .UsingEntity<Dictionary<string, object>>(
                "characters_expertises",
                j => j.HasOne<Expertise>().WithMany().HasForeignKey("expertise_id"),
                j => j.HasOne<Character>().WithMany().HasForeignKey("character_id"),
                j => j.HasKey("expertise_id", "character_id"));
        
        builder.HasMany(c => c.Classes)
            .WithMany(c => c.Characters)
            .UsingEntity<Dictionary<string, object>>(
                "characters_classes",
                j => j.HasOne<Class>().WithMany().HasForeignKey("class_id"),
                j => j.HasOne<Character>().WithMany().HasForeignKey("character_id"),
                j => j.HasKey("class_id", "character_id"));
    }
}