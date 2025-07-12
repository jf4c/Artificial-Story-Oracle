using System.Net;

namespace ASO.Domain.Shared.Exceptions;

public class UnauthorizedException : DomainException
{
    public UnauthorizedException(string message) 
        : base(HttpStatusCode.Unauthorized, message)
    {
    }

    public UnauthorizedException() 
        : base(HttpStatusCode.Unauthorized, "Usuário não autenticado")
    {
    }
}
