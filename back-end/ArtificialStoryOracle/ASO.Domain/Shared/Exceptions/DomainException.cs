using System.Net;

namespace ASO.Domain.Shared.Exceptions;

public abstract class DomainException : Exception
{
    public HttpStatusCode StatusCode { get; set; } 
    
    protected DomainException(HttpStatusCode statusCode, string? message = null) : base(message)
    {
        StatusCode = statusCode;
    }
    
    protected DomainException(string? message = null) : base(message)
    { 
    }
}