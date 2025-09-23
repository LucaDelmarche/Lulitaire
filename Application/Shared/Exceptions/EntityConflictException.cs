namespace Application.Shared.Exceptions;

public class EntityConflictException<T> : Exception
{
    public EntityConflictException(string message) 
        : base($"Error occuring with a entity of type {typeof(T).Name} : {message}")
    {
    }
}