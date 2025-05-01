using ASO.Domain.Shared.Exceptions;

namespace ASO.Domain.Shared.ValueObjects.Exceptions.Name;

public class InvalidNameException(string message) : DomainException(message);