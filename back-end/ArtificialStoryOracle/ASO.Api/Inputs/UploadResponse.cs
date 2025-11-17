namespace ASO.Api.Inputs;

public sealed record UploadResponse
{
    public required string Url { get; init; }
    public required string Filename { get; init; }
}
