using Domain;

namespace Application.user.queries.getAll;

public class UserGetallOutput
{
    public List<UserDto> Users { get; set; } = new List<UserDto>();
    public class UserDto
    {
        public int Id { get; set; }
        
        public string Username { get; set; }
        public string Mail { get; set; }
        public int Role { get; set; }
    }
}

