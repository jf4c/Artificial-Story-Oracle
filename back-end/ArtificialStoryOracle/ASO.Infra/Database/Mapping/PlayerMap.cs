using ASO.Domain.Game.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ASO.Infra.Database.Mapping;

public sealed class PlayerMap : IEntityTypeConfiguration<Player>
{
    public void Configure(EntityTypeBuilder<Player> builder)
    {
        builder.ToTable("players");
        
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).HasColumnName("id");
        
        builder.Property(p => p.KeycloakUserId)
            .HasColumnName("keycloak_user_id")
            .IsRequired();
        
        builder.HasIndex(p => p.KeycloakUserId)
            .IsUnique();
        
        builder.Property(p => p.TypePlayer)
            .HasColumnName("type_player")
            .IsRequired();
        
        // Value Objects
        builder.OwnsOne(p => p.Name, name =>
        {
            name.Property(n => n.FirstName)
                .HasColumnName("first_name")
                .HasMaxLength(60)
                .IsRequired();
            
            name.Property(n => n.LastName)
                .HasColumnName("last_name")
                .HasMaxLength(60)
                .IsRequired();
        });
        
        builder.OwnsOne(p => p.Email, email =>
        {
            email.Property(e => e.Address)
                .HasColumnName("email")
                .HasMaxLength(60)
                .IsRequired();
            
            email.HasIndex(e => e.Address);
        });
        
        builder.OwnsOne(p => p.NickName, nickname =>
        {
            nickname.Property(n => n.Nick)
                .HasColumnName("nickname")
                .HasMaxLength(20)
                .IsRequired();
        });
        
        // Tracker (herdado de Entity)
        builder.OwnsOne(p => p.Tracker, tracker =>
        {
            tracker.Property(t => t.CreatedAtUtc)
                .HasColumnName("created_at")
                .IsRequired();
            
            tracker.Property(t => t.UpdatedAtUtc)
                .HasColumnName("updated_at");
        });
        
        // Ignorar eventos de domínio (não persiste)
        builder.Ignore(p => p.Events);
    }
}

