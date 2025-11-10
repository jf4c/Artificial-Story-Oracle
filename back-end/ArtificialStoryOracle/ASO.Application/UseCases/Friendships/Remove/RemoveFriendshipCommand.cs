﻿using ASO.Application.Abstractions.Shared;

namespace ASO.Application.UseCases.Friendships.Remove;

public sealed record RemoveFriendshipCommand(Guid CurrentPlayerId, Guid FriendshipId) : ICommand;

