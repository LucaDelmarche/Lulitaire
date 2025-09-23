using Domain;

namespace Application.Core.User.Queries.GetByUsernameOrMailAndPassword;

public class UserGetByUsernameOrMailAndPasswordOutput
{
    public UserGetBytUsernameDto UserGetBytUsername { get; set;}
    public class UserGetBytUsernameDto
    {
        public int Id { get; set; }
        
        public string Username { get; set; }

        public string Mail { get; set; }

        public double Score { get; set; }

        public int Role{ get; set; }
    }
}