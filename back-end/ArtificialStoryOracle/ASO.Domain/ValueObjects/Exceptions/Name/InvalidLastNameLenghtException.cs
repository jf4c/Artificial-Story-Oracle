using ASO.Domain.Exceptions;

namespace ASO.Domain.ValueObjects.Exceptions.Name;

public class InvalidLastNameLenghtException(string mensage) : DomainException(mensage);