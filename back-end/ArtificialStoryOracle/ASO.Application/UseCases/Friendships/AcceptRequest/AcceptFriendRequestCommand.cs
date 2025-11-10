﻿using ASO.Application.Abstractions.Shared;

namespace ASO.Application.UseCases.Friendships.AcceptRequest;

public sealed record AcceptFriendRequestCommand(Guid CurrentPlayerId, Guid FriendshipId) : ICommand;

