using ASO.Domain.Game.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ASO.Infra.Database.Mapping;

public class ClassesMap : IEntityTypeConfiguration<Class>
{
    public void Configure(EntityTypeBuilder<Class> builder)
    {
        builder.ToTable("classes");
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id)
            .HasColumnName("id");
        builder.Property(c => c.Name)
            .HasColumnName("name")
            .IsRequired();
        builder.Property(c => c.Description)
            .HasColumnName("description")
            .IsRequired();
        builder.OwnsOne(c => c.Statistics, statistics =>
        {
            statistics.Property(s => s.InitHealthPoints)
                .HasColumnName("init_health_points")
                .IsRequired();
            statistics.Property(s => s.InitManaPoints)
                .HasColumnName("init_mana_points")
                .IsRequired();
        });
    }
}