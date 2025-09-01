using ASO.Domain.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ASO.Infra.Database.Mapping;

public static class EntityTypeBuilderExtensions
{
    public static void ConfigureTracker<T>(this EntityTypeBuilder<T> builder) 
        where T : Entity
    {
        builder.OwnsOne(e => e.Tracker, trackers =>
        {
            trackers.Property(t => t.CreatedAtUtc)
                .HasColumnName("created_at_utc")
                .IsRequired();

            trackers.Property(t => t.UpdatedAtUtc)
                .HasColumnName("updated_at_utc")
                .IsRequired();
        });
    }
}