using ASO.Domain.Game.Enums;
using ASO.Domain.Shared.Aggregates.Abstractions;
using ASO.Domain.Shared.Entities;

namespace ASO.Domain.Game.Entities;

public sealed class Campaign : Entity, IAggragateRoot
{
    private Campaign()
    {
        Name = null!;
        Creator = null!;
        Participants = new List<CampaignParticipant>();
    }

    private Campaign(Guid creatorId, string name, string? description, int maxPlayers, bool isPublic, string? storyIntroduction = null)
    {
        CreatorId = creatorId;
        Name = name;
        Description = description;
        MaxPlayers = maxPlayers;
        IsPublic = isPublic;
        StoryIntroduction = storyIntroduction;
        Status = CampaignStatus.Planning;
        Participants = new List<CampaignParticipant>();
    }

    public static Campaign Create(Guid creatorId, string name, string? description, int maxPlayers, bool isPublic, string? storyIntroduction = null)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Nome da campanha é obrigatório.", nameof(name));

        if (name.Length < 3 || name.Length > 100)
            throw new ArgumentException("Nome da campanha deve ter entre 3 e 100 caracteres.", nameof(name));

        if (description?.Length > 1000)
            throw new ArgumentException("Descrição não pode ter mais de 1000 caracteres.", nameof(description));

        if (maxPlayers < 2 || maxPlayers > 12)
            throw new ArgumentException("Máximo de jogadores deve estar entre 2 e 12.", nameof(maxPlayers));

        if (storyIntroduction?.Length > 5000)
            throw new ArgumentException("História introdutória não pode ter mais de 5000 caracteres.", nameof(storyIntroduction));

        return new Campaign(creatorId, name, description, maxPlayers, isPublic, storyIntroduction);
    }

    public void Update(string name, string? description, int maxPlayers, CampaignStatus status)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Nome da campanha é obrigatório.", nameof(name));

        if (name.Length < 3 || name.Length > 100)
            throw new ArgumentException("Nome da campanha deve ter entre 3 e 100 caracteres.", nameof(name));

        if (description?.Length > 1000)
            throw new ArgumentException("Descrição não pode ter mais de 1000 caracteres.", nameof(description));

        if (maxPlayers < 2 || maxPlayers > 12)
            throw new ArgumentException("Máximo de jogadores deve estar entre 2 e 12.", nameof(maxPlayers));

        Name = name;
        Description = description;
        MaxPlayers = maxPlayers;
        Status = status;
    }

    public void Start()
    {
        if (Status != CampaignStatus.Planning)
            throw new InvalidOperationException("Apenas campanhas em planejamento podem ser iniciadas.");

        var playerCount = Participants.Count(p => p.Role == ParticipantRole.Player && p.IsActive);
        if (playerCount < 1)
            throw new InvalidOperationException("Deve haver pelo menos 1 jogador para iniciar a campanha.");

        Status = CampaignStatus.Active;
        StartedAt = DateTime.UtcNow;
    }

    public void Pause()
    {
        if (Status != CampaignStatus.Active)
            throw new InvalidOperationException("Apenas campanhas ativas podem ser pausadas.");

        Status = CampaignStatus.OnHold;
    }

    public void Resume()
    {
        if (Status != CampaignStatus.OnHold)
            throw new InvalidOperationException("Apenas campanhas pausadas podem ser retomadas.");

        Status = CampaignStatus.Active;
    }

    public void Complete()
    {
        if (Status != CampaignStatus.Active)
            throw new InvalidOperationException("Apenas campanhas ativas podem ser finalizadas.");

        Status = CampaignStatus.Completed;
        EndedAt = DateTime.UtcNow;
    }

    public void SetGameMaster(Guid? playerId)
    {
        GameMasterId = playerId;
    }

    public void SetStoryIntroduction(string? storyIntroduction)
    {
        if (storyIntroduction?.Length > 5000)
            throw new ArgumentException("História introdutória não pode ter mais de 5000 caracteres.", nameof(storyIntroduction));

        StoryIntroduction = storyIntroduction;
    }

    public void SetBannerImage(string? bannerImageUrl)
    {
        BannerImage = bannerImageUrl;
    }

    public Guid CreatorId { get; private set; }
    public Guid? GameMasterId { get; private set; }
    public string Name { get; private set; }
    public string? Description { get; private set; }
    public CampaignStatus Status { get; private set; }
    public DateTime? StartedAt { get; private set; }
    public DateTime? EndedAt { get; private set; }
    public int MaxPlayers { get; private set; }
    public bool IsPublic { get; private set; }
    public string? StoryIntroduction { get; private set; }
    public string? BannerImage { get; private set; }

    public Player Creator { get; private set; }
    public Player? GameMaster { get; private set; }
    public ICollection<CampaignParticipant> Participants { get; private set; }
}

