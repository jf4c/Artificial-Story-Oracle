using ASO.Domain.Game.Entities;
using ASO.Domain.Game.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ASO.Infra.Database.Configurations;

public sealed class FriendshipConfiguration : IEntityTypeConfiguration<Friendship>
{
    public void Configure(EntityTypeBuilder<Friendship> builder)
    {
        builder.ToTable("friendships");
        
        builder.HasKey(f => f.Id);
        builder.Property(f => f.Id).HasColumnName("id");
        
        builder.Property(f => f.RequesterId)
            .HasColumnName("requester_id")
            .IsRequired();
        
        builder.Property(f => f.AddresseeId)
            .HasColumnName("addressee_id")
            .IsRequired();
        
        builder.Property(f => f.Status)
            .HasColumnName("status")
            .HasConversion<int>()
            .IsRequired();
        
        builder.Property(f => f.AcceptedAt)
            .HasColumnName("accepted_at");
        
        builder.Property(f => f.RejectedAt)
            .HasColumnName("rejected_at");
        
        builder.OwnsOne(f => f.Tracker, tracker =>
        {
            tracker.Property(t => t.CreatedAtUtc)
                .HasColumnName("created_at")
                .IsRequired();
            
            tracker.Property(t => t.UpdatedAtUtc)
                .HasColumnName("updated_at")
                .IsRequired();
        });
        
        builder.HasOne(f => f.Requester)
            .WithMany(p => p.SentFriendRequests)
            .HasForeignKey(f => f.RequesterId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasOne(f => f.Addressee)
            .WithMany(p => p.ReceivedFriendRequests)
            .HasForeignKey(f => f.AddresseeId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasIndex(f => new { f.RequesterId, f.AddresseeId })
            .IsUnique();
        
        builder.HasIndex(f => f.RequesterId);
        builder.HasIndex(f => f.AddresseeId);
        builder.HasIndex(f => f.Status);
    }
}

