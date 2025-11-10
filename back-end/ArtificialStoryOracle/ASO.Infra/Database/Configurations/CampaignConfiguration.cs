using ASO.Domain.Game.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ASO.Infra.Database.Configurations;

public sealed class CampaignConfiguration : IEntityTypeConfiguration<Campaign>
{
    public void Configure(EntityTypeBuilder<Campaign> builder)
    {
        builder.ToTable("campaigns");
        
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasColumnName("id");
        
        builder.Property(c => c.Name)
            .HasColumnName("name")
            .IsRequired()
            .HasMaxLength(100);
        
        builder.Property(c => c.Description)
            .HasColumnName("description")
            .HasMaxLength(1000);
        
        builder.Property(c => c.CreatorId)
            .HasColumnName("creator_id")
            .IsRequired();
        
        builder.Property(c => c.GameMasterId)
            .HasColumnName("game_master_id");
        
        builder.Property(c => c.Status)
            .HasColumnName("status")
            .HasConversion<int>()
            .IsRequired();
        
        builder.Property(c => c.StartedAt)
            .HasColumnName("started_at");
        
        builder.Property(c => c.EndedAt)
            .HasColumnName("ended_at");
        
        builder.Property(c => c.MaxPlayers)
            .HasColumnName("max_players")
            .IsRequired();
        
        builder.Property(c => c.IsPublic)
            .HasColumnName("is_public")
            .IsRequired();
        
        builder.OwnsOne(c => c.Tracker, tracker =>
        {
            tracker.Property(t => t.CreatedAtUtc)
                .HasColumnName("created_at")
                .IsRequired();
            
            tracker.Property(t => t.UpdatedAtUtc)
                .HasColumnName("updated_at")
                .IsRequired();
        });
        
        builder.HasOne(c => c.Creator)
            .WithMany(p => p.CreatedCampaigns)
            .HasForeignKey(c => c.CreatorId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_campaigns_players_creator");
        
        builder.HasOne(c => c.GameMaster)
            .WithMany(p => p.MasteredCampaigns)
            .HasForeignKey(c => c.GameMasterId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_campaigns_players_gamemaster");
        
        builder.HasIndex(c => c.CreatorId);
        builder.HasIndex(c => c.GameMasterId);
        builder.HasIndex(c => c.Status);
    }
}
