﻿using ASO.Domain.Game.Abstractions.Repositories;
using ASO.Domain.Game.Entities;
using ASO.Domain.Identity.Events;
using ASO.Domain.Shared.Abstractions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ASO.Application.EventHandlers;

public sealed class PlayerUserCreatedEventHandler(
    IPlayerRepository playerRepository,
    IUnitOfWork unitOfWork,
    ILogger<PlayerUserCreatedEventHandler> logger) : INotificationHandler<PlayerUserCreatedEvent>
{
    private readonly IPlayerRepository _playerRepository = playerRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ILogger<PlayerUserCreatedEventHandler> _logger = logger;

    public async Task Handle(PlayerUserCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Evento PlayerUserCreatedEvent recebido: KeycloakUserId={KeycloakUserId}, Email={Email}",
            notification.KeycloakUserId,
            notification.Email);

        // Verificar se já existe Player para este KeycloakUserId
        var existingPlayer = await _playerRepository.GetByKeycloakUserIdAsync(
            notification.KeycloakUserId, 
            cancellationToken);

        if (existingPlayer is not null)
        {
            _logger.LogWarning(
                "Player já existe para KeycloakUserId={KeycloakUserId}. Pulando criação.",
                notification.KeycloakUserId);
            return;
        }

        // Separar nome completo em firstName e lastName
        var nameParts = notification.Name.Split(' ', 2);
        var firstName = nameParts.Length > 0 ? nameParts[0] : "Default";
        var lastName = nameParts.Length > 1 ? nameParts[1] : string.Empty;

        // Criar Player no contexto Game
        var player = Player.Create(
            notification.KeycloakUserId,
            firstName,
            lastName,
            notification.Email,
            notification.NickName);

        await _playerRepository.CreateAsync(player, cancellationToken);
        
        // Salvar no banco de dados
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Player criado no contexto Game: Id={PlayerId}, KeycloakUserId={KeycloakUserId}",
            player.Id,
            player.KeycloakUserId);
    }
}

