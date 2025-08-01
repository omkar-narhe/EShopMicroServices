﻿namespace Ordering.Domain.Exceptions;

public class DomainException : Exception
{
    public DomainException(string message)
        : base($"Domain Exception: \"{message}\" thrown from Domain Layer")
    {
    }
}