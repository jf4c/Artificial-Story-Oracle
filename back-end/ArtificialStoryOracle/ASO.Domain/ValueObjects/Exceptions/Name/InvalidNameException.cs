using ASO.Domain.Exceptions;

namespace ASO.Domain.ValueObjects.Exceptions.Name;

public class InvalidNameException(string message) : DomainException(message);