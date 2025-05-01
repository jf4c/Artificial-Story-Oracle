using ASO.Domain.Shared.Exceptions;

namespace ASO.Domain.Shared.ValueObjects.Exceptions.Name;

public sealed class InvalidFirstNameLenghtException(string mensage) 
    : DomainException(mensage);