using System.ComponentModel.DataAnnotations;

namespace ASO.Api.Inputs;

public class GenerateCampaignStoryFromCharactersInput
{
    [Required(ErrorMessage = "CharacterIds é obrigatório")]
    [MinLength(1, ErrorMessage = "Deve haver pelo menos um personagem")]
    public required List<Guid> CharacterIds { get; set; }
    
    public string? CampaignName { get; set; }
    public string? CampaignDescription { get; set; }
}
