using System.Net;

namespace ASO.Domain.Shared.Exceptions;

public class BusinessRuleException : DomainException
{
    public BusinessRuleException(string message) 
        : base(HttpStatusCode.BadRequest, message)
    {
    }
}
