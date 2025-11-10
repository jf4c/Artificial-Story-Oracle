﻿using ASO.Application.Abstractions.Shared;
using ASO.Domain.Game.Abstractions.Repositories;
using ASO.Domain.Shared.Abstractions;

namespace ASO.Application.UseCases.Friendships.Remove;

public sealed class RemoveFriendshipHandler(
    IFriendshipRepository friendshipRepository,
    IUnitOfWork unitOfWork) : ICommandHandlerAsync<RemoveFriendshipCommand>
{
    private readonly IFriendshipRepository _friendshipRepository = friendshipRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task HandleAsync(RemoveFriendshipCommand command)
    {
        var friendship = await _friendshipRepository.GetByIdAsync(command.FriendshipId)
            ?? throw new InvalidOperationException("Amizade não encontrada.");

        if (friendship.RequesterId != command.CurrentPlayerId && friendship.AddresseeId != command.CurrentPlayerId)
            throw new UnauthorizedAccessException("Você não tem permissão para remover esta amizade.");

        await _friendshipRepository.DeleteAsync(friendship);
        await _unitOfWork.SaveChangesAsync();
    }
}

