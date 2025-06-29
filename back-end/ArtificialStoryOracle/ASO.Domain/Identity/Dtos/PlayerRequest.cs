﻿namespace ASO.Domain.Identity.Dtos;

public sealed record PlayerRequest
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string NickName { get; set; } = string.Empty;
    public Guid KeycloakUserId { get; set; }
}