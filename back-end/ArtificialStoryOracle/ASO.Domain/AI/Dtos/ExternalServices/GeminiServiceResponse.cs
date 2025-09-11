namespace ASO.Domain.AI.Dtos.ExternalServices;

public record GeminiServiceResponse
{
    public List<Candidate> Candidates { get; set; } = new List<Candidate>();
    public UsageMetadata UsageMetadata { get; set; } = new UsageMetadata();
    public string ModelVersion { get; set; } = "1.0.0";
    public string ResponseId { get; set; } = string.Empty;
}

public class Candidate
{
    public Content Content { get; set; } = new Content();
    public string FinishReason { get; set; } = string.Empty;
    public double AvgLogprobs { get; set; }
}

public class Content
{
    public List<Part> Parts { get; set; } = new List<Part>();
    public string Role { get; set; } = string.Empty;
}

public class UsageMetadata
{
    public int PromptTokenCount { get; set; }
    public int CandidatesTokenCount { get; set; }
    public int TotalTokenCount { get; set; }
    public List<TokenDetail> PromptTokensDetails { get; set; } = new List<TokenDetail>();
    public List<TokenDetail> CandidatesTokensDetails { get; set; } = new List<TokenDetail>();
}

public class TokenDetail
{
    public string Modality { get; set; } = string.Empty;
    public int TokenCount { get; set; }
}
