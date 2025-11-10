﻿using ASO.Application.UseCases.Players.GetByUserId;

namespace ASO.Application.Abstractions.UseCase.Players;

public interface IGetPlayerByUserIdHandler
{
    Task<GetPlayerByUserIdResponse?> Handle(GetPlayerByUserIdQuery request);
}