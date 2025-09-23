using Domain;

namespace Application.Core.User.Queries.GetById;

public class UserGetByIdOutput
{
    public UserGetByIdDto UserGetById { get; set;}
    public class UserGetByIdDto
    {
        
        public string Username { get; set; }
        
        public double Score { get; set; }


    }
}