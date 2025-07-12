using System.Net;

namespace ASO.Domain.Shared.Exceptions;

public class ForbiddenException : DomainException
{
    public ForbiddenException(string message) 
        : base(HttpStatusCode.Forbidden, message)
    {
    }

    public ForbiddenException() 
        : base(HttpStatusCode.Forbidden, "Usuário não possui permissão para acessar este recurso")
    {
    }
}
