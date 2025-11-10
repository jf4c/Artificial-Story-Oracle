using ASO.Domain.Shared.Events.Abstractions;

namespace ASO.Domain.Identity.Events;

public sealed record PlayerUserCreatedEvent : IDomainEvent
{
    public Guid KeycloakUserId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string NickName { get; init; } = string.Empty;
    public DateTime OccurredAt { get; init; } = DateTime.UtcNow;
}

