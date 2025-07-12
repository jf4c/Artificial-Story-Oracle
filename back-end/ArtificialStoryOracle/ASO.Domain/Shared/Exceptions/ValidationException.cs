using System.Net;

namespace ASO.Domain.Shared.Exceptions;

public class ValidationException : DomainException
{
    public IEnumerable<string> Errors { get; }

    public ValidationException(string message, IEnumerable<string> errors) 
        : base(HttpStatusCode.BadRequest, message)
    {
        Errors = errors;
    }

    public ValidationException(string message) 
        : this(message, new List<string>())
    {
    }
}
