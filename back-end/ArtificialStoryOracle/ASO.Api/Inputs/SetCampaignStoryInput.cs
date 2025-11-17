using System.ComponentModel.DataAnnotations;

namespace ASO.Api.Inputs;

public sealed record SetCampaignStoryInput
{
    [Required(ErrorMessage = "StoryIntroduction é obrigatório")]
    [MaxLength(5000, ErrorMessage = "História introdutória não pode ter mais de 5000 caracteres")]
    public required string StoryIntroduction { get; init; }
}
