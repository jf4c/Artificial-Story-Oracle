﻿using ASO.Application.Abstractions.UseCase.Ancestry;
using ASO.Application.Mappers;
using ASO.Domain.Game.QueriesServices;

namespace ASO.Application.UseCases.Ancestry.GetAllAncestry;

public sealed class GetAllAncestryHandler(IAncestryQueryService ancestryQueryService)
    : IGetAllAncestryHandler
{
    
    private readonly IAncestryQueryService _ancestryQueryService = ancestryQueryService;

    public async Task<GetAllAncestryResponse> Handle()
    {
        var ancestries = await _ancestryQueryService.GetAllAncestries();
        
        return ancestries.ToGetAllAncestryResponse();
    }
}