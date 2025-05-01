using ASO.Domain.Shared.Exceptions;

namespace ASO.Domain.Shared.ValueObjects.Exceptions.Email;

public sealed class InvelidEmailLenghtException(string message) : DomainException(message);