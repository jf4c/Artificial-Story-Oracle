﻿using ASO.Application.Abstractions.Shared;
using ASO.Domain.Game.Abstractions.Repositories;
using ASO.Domain.Shared.Abstractions;

namespace ASO.Application.UseCases.Friendships.RejectRequest;

public sealed class RejectFriendRequestHandler(
    IFriendshipRepository friendshipRepository,
    IUnitOfWork unitOfWork) : ICommandHandlerAsync<RejectFriendRequestCommand>
{
    private readonly IFriendshipRepository _friendshipRepository = friendshipRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task HandleAsync(RejectFriendRequestCommand command)
    {
        var friendship = await _friendshipRepository.GetByIdAsync(command.FriendshipId)
            ?? throw new InvalidOperationException("Convite de amizade não encontrado.");

        if (friendship.AddresseeId != command.CurrentPlayerId)
            throw new UnauthorizedAccessException("Apenas o destinatário pode recusar este convite.");

        friendship.Reject();
        await _unitOfWork.SaveChangesAsync();
    }
}

