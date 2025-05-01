using ASO.Domain.Shared.Exceptions;

namespace ASO.Domain.Shared.ValueObjects.Exceptions.Name;

public class InvalidLastNameLenghtException(string mensage) : DomainException(mensage);