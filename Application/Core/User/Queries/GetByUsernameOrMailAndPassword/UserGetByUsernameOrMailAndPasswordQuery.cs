namespace Application.user.queries.GetByUsernameOrMailAndPassword;

public class UserGetByUsernameOrMailAndPasswordQuery
{
    public string UsernameOrMail { get; set; }
    public string Password { get; set; }
    
    public string Role { get; set; }
    
}