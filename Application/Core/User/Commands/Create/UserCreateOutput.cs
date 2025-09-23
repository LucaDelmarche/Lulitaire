using Domain;

namespace Application.user.commands.create;

public class UserCreateOutput
{
    public UserCreateDto userdto;
    public class UserCreateDto
    {
        public string Username { get; set; }
        public string Mail { get; set; }
        public int Role { get; set; }
    }
}