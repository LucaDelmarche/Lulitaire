namespace Application.Shared.Exceptions;

public class AlreadyExistsException<T>: Exception
{
    public AlreadyExistsException(string email) 
        : base($"A user with this email :{email} already exists.")
    {
}   
}