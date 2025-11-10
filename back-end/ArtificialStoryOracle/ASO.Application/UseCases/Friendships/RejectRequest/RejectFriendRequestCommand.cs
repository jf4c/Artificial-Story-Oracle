﻿using ASO.Application.Abstractions.Shared;

namespace ASO.Application.UseCases.Friendships.RejectRequest;

public sealed record RejectFriendRequestCommand(Guid CurrentPlayerId, Guid FriendshipId) : ICommand;

