using ASO.Domain.AI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ASO.Infra.Database.Mapping;

public class GeneratedAIContentMap : IEntityTypeConfiguration<GeneratedAIContent>
{
    public void Configure(EntityTypeBuilder<GeneratedAIContent> builder)
    {
        builder.ToTable("generated_AI_content");
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasColumnName("id");
        builder.Property(c => c.Type).HasColumnName("type").IsRequired();
        builder.Property(c => c.Prompt).HasColumnName("prompt").IsRequired();
        builder.Property(c => c.Content).HasColumnName("content").IsRequired();
        builder.ConfigureTracker();
    }
}