namespace Application.Shared.Exceptions;

public class EntityIncorrectPasswordExceptionOrIdentifier: Exception
{
    public EntityIncorrectPasswordExceptionOrIdentifier() 
        : base("Entity incorrect password or email address.")
    {
    }
}