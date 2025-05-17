using System.Windows.Input;
using ICommand = ASO.Application.Abstractions.Shared.ICommand;

namespace ASO.Application.UseCases.Games.Create;

public sealed record CreateGameCommand : ICommand
{
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
}