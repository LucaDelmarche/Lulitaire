using Domain;

namespace Application.Core.User.Queries.GetByUsernameOrMail;

public class UserGetByUsernameOrMailOutput
{
    public UserGetByUsernameDto UserGetByUsername { get; set;}
    public class UserGetByUsernameDto
    {
        public int Id { get; set; }
        
        public string Username { get; set; }

        public string Mail { get; set; }

        public double Score { get; set; }

        public int Role{ get; set; }
    }
}