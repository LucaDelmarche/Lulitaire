using Domain;

namespace Application.user.queries.getAll;

public class UserGetCurrentUserOutput
{
    public CurrentUserDto User { get; set; }
    public class CurrentUserDto
    {
        public int Id { get; set; }
        
        public string Username { get; set; }
        public string Mail { get; set; }
        public double Score { get; set; }
        public int Role { get; set; }
    }
    
}

