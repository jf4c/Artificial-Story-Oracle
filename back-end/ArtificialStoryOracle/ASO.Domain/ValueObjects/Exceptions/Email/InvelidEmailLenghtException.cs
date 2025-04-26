using ASO.Domain.Exceptions;

namespace ASO.Domain.ValueObjects.Exceptions.Email;

public sealed class InvelidEmailLenghtException(string message) : DomainException(message);