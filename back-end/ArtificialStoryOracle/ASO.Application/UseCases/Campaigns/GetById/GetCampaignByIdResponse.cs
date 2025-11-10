using ASO.Application.Abstractions.Shared;
using ASO.Domain.Game.Enums;

namespace ASO.Application.UseCases.Campaigns.GetById;

public sealed record GetCampaignByIdResponse : IResponse
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required string? Description { get; init; }
    public required PlayerBasicInfo Creator { get; init; }
    public required PlayerBasicInfo? GameMaster { get; init; }
    public required CampaignStatus Status { get; init; }
    public required DateTime CreatedAt { get; init; }
    public required DateTime? StartedAt { get; init; }
    public required DateTime? EndedAt { get; init; }
    public required int MaxPlayers { get; init; }
    public required bool IsPublic { get; init; }
    public required List<ParticipantWithDetails> Participants { get; init; }
    public required bool CanEdit { get; init; }
    public required bool CanManageParticipants { get; init; }
}

public sealed record PlayerBasicInfo
{
    public required Guid Id { get; init; }
    public required string NickName { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
}

public sealed record ParticipantWithDetails
{
    public required Guid Id { get; init; }
    public required PlayerBasicInfo Player { get; init; }
    public required CharacterBasicInfo? Character { get; init; }
    public required ParticipantRole Role { get; init; }
    public required DateTime JoinedAt { get; init; }
}

public sealed record CharacterBasicInfo
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required string Race { get; init; }
    public required string Class { get; init; }
    public required int Level { get; init; }
}

