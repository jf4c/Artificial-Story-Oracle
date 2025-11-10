﻿using ASO.Application.Abstractions.Shared;
using ASO.Application.Mappers;
using ASO.Domain.Game.Abstractions.Repositories;
using ASO.Domain.Shared.Abstractions;

namespace ASO.Application.UseCases.Friendships.AcceptRequest;

public sealed class AcceptFriendRequestHandler(
    IFriendshipRepository friendshipRepository,
    IUnitOfWork unitOfWork) : ICommandHandlerAsync<AcceptFriendRequestCommand, AcceptFriendRequestResponse>
{
    private readonly IFriendshipRepository _friendshipRepository = friendshipRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<AcceptFriendRequestResponse> HandleAsync(AcceptFriendRequestCommand command)
    {
        var friendship = await _friendshipRepository.GetByIdAsync(command.FriendshipId)
            ?? throw new InvalidOperationException("Convite de amizade não encontrado.");

        if (friendship.AddresseeId != command.CurrentPlayerId)
            throw new UnauthorizedAccessException("Apenas o destinatário pode aceitar este convite.");

        friendship.Accept();
        await _unitOfWork.SaveChangesAsync();

        return friendship.ToAcceptFriendRequestResponse();
    }
}

