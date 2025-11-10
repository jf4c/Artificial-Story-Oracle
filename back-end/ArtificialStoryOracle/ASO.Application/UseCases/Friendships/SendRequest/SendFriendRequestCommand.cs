﻿using ASO.Application.Abstractions.Shared;

namespace ASO.Application.UseCases.Friendships.SendRequest;

public sealed record SendFriendRequestCommand(Guid CurrentPlayerId, Guid AddresseeId) : ICommand;

