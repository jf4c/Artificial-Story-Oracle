﻿using ASO.Domain.Game.Entities;
using ASO.Domain.Game.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ASO.Infra.Database.Configurations;

public sealed class CampaignParticipantConfiguration : IEntityTypeConfiguration<CampaignParticipant>
{
    public void Configure(EntityTypeBuilder<CampaignParticipant> builder)
    {
        builder.ToTable("campaign_participants");
        
        builder.HasKey(cp => cp.Id);
        builder.Property(cp => cp.Id).HasColumnName("id");
        
        builder.Property(cp => cp.CampaignId)
            .HasColumnName("campaign_id")
            .IsRequired();
        
        builder.Property(cp => cp.PlayerId)
            .HasColumnName("player_id")
            .IsRequired();
        
        builder.Property(cp => cp.CharacterId)
            .HasColumnName("character_id");
        
        builder.Property(cp => cp.Role)
            .HasColumnName("role")
            .HasConversion<int>()
            .IsRequired();
        
        builder.Property(cp => cp.JoinedAt)
            .HasColumnName("joined_at")
            .IsRequired();
        
        builder.Property(cp => cp.IsActive)
            .HasColumnName("is_active")
            .IsRequired();
        
        builder.OwnsOne(cp => cp.Tracker, tracker =>
        {
            tracker.Property(t => t.CreatedAtUtc)
                .HasColumnName("created_at")
                .IsRequired();
            
            tracker.Property(t => t.UpdatedAtUtc)
                .HasColumnName("updated_at")
                .IsRequired();
        });
        
        builder.HasOne(cp => cp.Campaign)
            .WithMany(c => c.Participants)
            .HasForeignKey(cp => cp.CampaignId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(cp => cp.Player)
            .WithMany(p => p.CampaignParticipations)
            .HasForeignKey(cp => cp.PlayerId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasOne(cp => cp.Character)
            .WithMany(c => c.CampaignParticipations)
            .HasForeignKey(cp => cp.CharacterId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasIndex(cp => new { cp.CampaignId, cp.PlayerId })
            .IsUnique();
        
        builder.HasIndex(cp => cp.CampaignId);
        builder.HasIndex(cp => cp.PlayerId);
    }
}

