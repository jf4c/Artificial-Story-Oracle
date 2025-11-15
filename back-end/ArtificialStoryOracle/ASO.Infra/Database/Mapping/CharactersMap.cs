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
        builder.Property(c => c.Id).HasColumnName("id");
        builder.Property(c => c.Name).HasColumnName("name").IsRequired();
        builder.Property(c => c.PlayerId).HasColumnName("player_id").IsRequired();
        builder.Property(c => c.TypeCharacter).HasColumnName("type_character").IsRequired();
        builder.Property(c => c.Level).HasColumnName("level");
        builder.Property(c => c.Backstory).HasColumnName("backstory");
        
        builder.HasOne(c => c.Ancestry)
            .WithMany()
            .HasForeignKey(c => c.AncestryId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(c => c.Image)
            .WithMany()
            .HasForeignKey(c=>c.ImageId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasMany(c => c.Skills)
            .WithMany(c => c.Characters)
            .UsingEntity<Dictionary<string, object>>(
                "characters_expertises",
                j => j.HasOne<Skill>().WithMany().HasForeignKey("expertise_id"),
                j => j.HasOne<Character>().WithMany().HasForeignKey("character_id"),
                j => j.HasKey("expertise_id", "character_id"));

        builder.HasMany(c => c.Classes)
            .WithMany(c => c.Characters)
            .UsingEntity<Dictionary<string, object>>(
                "characters_classes",
                j => j.HasOne<Class>().WithMany().HasForeignKey("class_id"),
                j => j.HasOne<Character>().WithMany().HasForeignKey("character_id"),
                j => j.HasKey("class_id", "character_id"));

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
        
        builder.ConfigureTracker();
    }
}