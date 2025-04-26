using ASO.Domain.Exceptions;

namespace ASO.Domain.ValueObjects.Exceptions.Name;

public sealed class InvalidFirstNameLenghtException(string mensage) 
    : DomainException(mensage);