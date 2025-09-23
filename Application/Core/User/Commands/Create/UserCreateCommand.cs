using Domain;

namespace Application.user.commands.create;

public class UserCreateCommand
{
    public string Mail { get; set; }
    public string Password { get; set; }
}