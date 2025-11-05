﻿using ASO.Domain.AI.Dtos.ExternalServices;

namespace ASO.Domain.Game.Abstractions.ExternalServices;

public interface IGeminiApiService
{
    Task<GeminiServiceResponse> GenerateCampaignBackstoryAsync(GeminiServiceRequest prompt);
    Task<GeminiServiceResponse> GenerateCharacterNamesAsync(GeminiServiceRequest prompt);
}