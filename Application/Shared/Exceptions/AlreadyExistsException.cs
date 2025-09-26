using Domain;

namespace Application.Shared.Exceptions;

public class AlreadyExistsException<T>: Exception
{
    public AlreadyExistsException(string email) 
        : base($"A {typeof(T)} {email} already exists.")
    {
        switch (typeof(T).Name)
        {
            case "User":
                throw new Exception($"A user with the email {email} already exists.");
            case "Zone":
                throw new Exception($"A zone with the name {email} already exists for the current user.");
            case "Card":
                throw new Exception($"A card with the name {email} already exists.");
            default:
                throw new Exception($"An entity of type {typeof(T).Name} with the identifier {email} already exists.");
        }
}   
}