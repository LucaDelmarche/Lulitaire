
using Domain;

namespace Application.Shared.Exceptions;

public class EntityNotFoundException<T>:Exception
{
    public EntityNotFoundException(int id) 
        : base($"{typeof(T).Name} with id {id} was not found")
    {
    }
    public EntityNotFoundException(string name) 
        : base(GetMessage(name))
    {
    }

    private static string GetMessage(string name)
    {
        switch (typeof(T))
        {
            case { } t when  t== typeof(User):
                return $"{typeof(T).Name} with username/email address {name} wasn't found";
            default:
                return "";
        }
    }
}