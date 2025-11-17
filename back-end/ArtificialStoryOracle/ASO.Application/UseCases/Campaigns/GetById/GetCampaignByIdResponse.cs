using ASO.Application.Abstractions.Shared;
using ASO.Domain.Game.Enums;

namespace ASO.Application.UseCases.Campaigns.GetById;

public sealed record GetCampaignByIdResponse : IResponse
{
    public required Guid Id { get; init; }
    public required Guid CreatorId { get; init; }
    public required string Name { get; init; }
    public required string? Description { get; init; }
    public required string? Image { get; init; } // URL do banner
    public required string UserRole { get; init; } // "creator", "gameMaster", "player"
    public required CampaignStatus Status { get; init; }
    public required DateTime CreatedAt { get; init; }
    public required int MaxPlayers { get; init; }
    public required bool IsPublic { get; init; }
    public string? StoryIntroduction { get; init; }
    public required CampaignSettings Settings { get; init; }
    public required List<ParticipantWithDetails> Participants { get; init; }
    public required bool CanEdit { get; init; }
    public required bool CanManageParticipants { get; init; }
    
    // Campos mockados temporariamente
    public List<SessionInfo>? Sessions { get; init; } = new();
    public CampaignStatistics? Statistics { get; init; }
    public WorldInfo? World { get; init; }
}

public sealed record PlayerBasicInfo
{
    public required Guid Id { get; init; }
    public required string NickName { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public string? UserAvatar { get; init; } // URL do avatar
}

public sealed record ParticipantWithDetails
{
    public required Guid Id { get; init; }
    public required Guid UserId { get; init; }
    public required string UserName { get; init; }
    public string? UserAvatar { get; init; }
    public required ParticipantRole Role { get; init; }
    public required DateTime JoinedAt { get; init; }
    public required CharacterBasicInfo? Character { get; init; } // Só o personagem vinculado
}

public sealed record CharacterBasicInfo
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required string Class { get; init; }
    public required string Race { get; init; }
    public required string Ancestry { get; init; }
    public required int Level { get; init; }
    public string? Description { get; init; }
    public string? Image { get; init; }
    public required bool IsActive { get; init; } // Se é o char ativo dele na campanha
}

public sealed record CampaignSettings
{
    public required string System { get; init; } // Ex: "D&D 5e", "Pathfinder", etc
    public required bool AllowCharacterCreation { get; init; }
}

// Campos mockados - implementar quando houver as features
public sealed record SessionInfo
{
    public Guid Id { get; init; }
    public string Title { get; init; } = string.Empty;
    public DateTime Date { get; init; }
}

public sealed record CampaignStatistics
{
    public int TotalSessions { get; init; }
    public int TotalPlayTime { get; init; }
}

public sealed record WorldInfo
{
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
}

