﻿using ASO.Application.Abstractions.Shared;
using ASO.Application.Mappers;
using ASO.Domain.Game.Abstractions.Repositories;
using ASO.Domain.Game.Entities;
using ASO.Domain.Game.Enums;
using ASO.Domain.Shared.Abstractions;

namespace ASO.Application.UseCases.Friendships.SendRequest;

public sealed class SendFriendRequestHandler(
    IFriendshipRepository friendshipRepository,
    IPlayerRepository playerRepository,
    IUnitOfWork unitOfWork) : ICommandHandlerAsync<SendFriendRequestCommand, SendFriendRequestResponse>
{
    private readonly IFriendshipRepository _friendshipRepository = friendshipRepository;
    private readonly IPlayerRepository _playerRepository = playerRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<SendFriendRequestResponse> HandleAsync(SendFriendRequestCommand command)
    {
        var requester = await _playerRepository.GetByIdAsync(command.CurrentPlayerId)
            ?? throw new InvalidOperationException("Jogador solicitante não encontrado.");

        var addressee = await _playerRepository.GetByIdAsync(command.AddresseeId)
            ?? throw new InvalidOperationException("Jogador destinatário não encontrado.");

        var existingFriendship = await _friendshipRepository.GetExistingFriendshipAsync(command.CurrentPlayerId, command.AddresseeId);

        if (existingFriendship != null)
        {
            if (existingFriendship.Status == FriendshipStatus.Accepted)
                throw new InvalidOperationException("Vocês já são amigos.");

            if (existingFriendship.Status == FriendshipStatus.Pending)
                throw new InvalidOperationException("Já existe um convite pendente entre vocês.");
        }

        var friendship = Friendship.Create(command.CurrentPlayerId, command.AddresseeId);
        await _friendshipRepository.CreateAsync(friendship);
        await _unitOfWork.SaveChangesAsync();

        return friendship.ToSendFriendRequestResponse();
    }
}

