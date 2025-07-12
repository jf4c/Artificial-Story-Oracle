using System.Net;

namespace ASO.Domain.Shared.Exceptions;

public class AncestriesNotFoundException : DomainException
{
    public AncestriesNotFoundException(string message)
        : base(HttpStatusCode.NotFound, message)
    {
    }

    public AncestriesNotFoundException()
        : base(HttpStatusCode.NotFound, "Nenhuma ancestralidade encontrada")
    {
    }

    public AncestriesNotFoundException(Guid id)
        : base(HttpStatusCode.NotFound,
            $"Nenhuma ancestralidade encontrada com ID {id}")
    {
    }
}