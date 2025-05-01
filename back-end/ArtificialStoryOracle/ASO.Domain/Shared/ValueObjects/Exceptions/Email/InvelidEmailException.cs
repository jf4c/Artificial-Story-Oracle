using ASO.Domain.Shared.Exceptions;

namespace ASO.Domain.Shared.ValueObjects.Exceptions.Email;

public class InvelidEmailException(string message) : DomainException(message);