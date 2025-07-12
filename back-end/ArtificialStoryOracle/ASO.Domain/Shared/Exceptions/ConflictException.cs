using System.Net;

namespace ASO.Domain.Shared.Exceptions;

public class ConflictException : DomainException
{
    public ConflictException(string message) 
        : base(HttpStatusCode.Conflict, message)
    {
    }
}
