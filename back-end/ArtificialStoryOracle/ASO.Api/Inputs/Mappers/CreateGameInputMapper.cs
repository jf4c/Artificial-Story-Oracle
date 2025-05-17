using ASO.Application.UseCases.Games.Create;

namespace ASO.Api.Inputs.Mappers;

public static class CreateGameInputMapper
{
    public static CreateGameCommand ToCommand(this CreateGameInput input)
    {
        return new CreateGameCommand
        {
            Name = input.Name,
            Description = input.Description,
        };
    }
}