using ASO.Domain.Exceptions;

namespace ASO.Domain.ValueObjects.Exceptions.Email;

public class InvelidEmailException(string message) : DomainException(message);