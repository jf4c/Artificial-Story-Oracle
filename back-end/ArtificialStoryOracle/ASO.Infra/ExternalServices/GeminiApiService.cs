using System.Net.Http.Json;
using ASO.Domain.AI.Dtos.ExternalServices;
using ASO.Domain.Game.Abstractions.ExternalServices;
using Microsoft.Extensions.Configuration;

namespace ASO.Infra.ExternalServices;

public class GeminiApiService(HttpClient httpClient, IConfiguration config) : IGeminiApiService
{
    private readonly HttpClient _httpClient = httpClient;
    // private readonly string _apiKey = config["ExternalServices:Gemini_API:Key"] 
    //                                   ?? throw new ArgumentNullException($"API Key for OpenIA is not configured.");
    // private readonly string _baseUrl = config["ExternalServices:Gemini_API:BaseUrl"] 
    //                                    ?? throw new ArgumentNullException($"Base URL for OpenIA is not configured.");
    //
    public async Task<GeminiServiceResponse> GenerateCampaignBackstoryAsync(GeminiServiceRequest request)
    {
        // _httpClient.BaseAddress = new Uri(_baseUrl);
        // _httpClient.DefaultRequestHeaders.Add("X-goog-api-key", $"{_apiKey}");
        
        var response = await _httpClient.PostAsJsonAsync("v1beta/models/gemini-2.0-flash:generateContent", request);
        
        if (!response.IsSuccessStatusCode)
            throw new Exception($"Error calling GPT API: {response.ReasonPhrase}");
        
        var gptResponse = await response.Content.ReadFromJsonAsync<GeminiServiceResponse>();
        
        if (gptResponse == null)
            throw new Exception("GPT API returned null response");

        return gptResponse;
    }
}