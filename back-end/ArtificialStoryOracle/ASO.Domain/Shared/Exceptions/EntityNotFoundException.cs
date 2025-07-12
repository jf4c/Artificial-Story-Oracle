using System.Net;

namespace ASO.Domain.Shared.Exceptions;

public class EntityNotFoundException : DomainException
{
    public EntityNotFoundException(string entityName, Guid id) 
        : base(HttpStatusCode.NotFound, $"{entityName} com ID {id} não encontrado")
    {
    }

    public EntityNotFoundException(string entityName, string identifier) 
        : base(HttpStatusCode.NotFound, $"{entityName} com identificador '{identifier}' não encontrado")
    {
    }

    public EntityNotFoundException(string message) 
        : base(HttpStatusCode.NotFound, message)
    {
    }
}
