using System.Text;
using System.Text.RegularExpressions;

namespace Domain;

public class User
{
    private string _username;
    public string Username
    {
        get => _username;
        set
        {
            if(value.Length>=50)
                throw new ArgumentException("Username cannot exceed 50 characters.");
            _username = value;
        }
    }
    private string _mail;
    public string Mail
    {
        get => _mail;
        set
        {
                        Regex mailCheck = new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");
            if(value.Length>=255 || !mailCheck.IsMatch(value))
                throw new ArgumentException("Email address cannot exceed 255 characters and must be a valid email address.");
            _mail = value;
        }
    }

    private byte[] _password;
    public byte[] Password=>_password;
    public string PlainPassword
    {
        set
        {
            Regex passwordCheck = new Regex(@"^(?=.*[A-Z])(?=.*[!@#$%^&*(),.?:{}|<>])(?=.{8,}).*");
            if (string.IsNullOrWhiteSpace(value) || !passwordCheck.IsMatch(value))
            {
                throw new ArgumentException("You must enter a password and the password must be at least 8 characters long with at least one upper case letter" +
                                            ", one lowercase letter, and one special character.");
            }

            // Hachage du mot de passe reçu en texte clair
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(value);

            // Convertir le mot de passe haché en byte[] pour le stockage
            _password = Encoding.UTF8.GetBytes(hashedPassword);
        }
    }

    private int _role;

    public int Role
    {
        get => _role;
        set
        {
            if (value < 0 || value > 1)
                _role = 0;
            else
                _role = value;
        }
    }
    public User(string username, string mail, string plainPassword, int role)
    {
        Username = username;
        Mail = mail;
        PlainPassword = plainPassword;
        Role = role;
    }

    public User()
    {
        Username = "";
        Mail = "x@gmail.com";
        PlainPassword = "Abcdef1?";
        Role = 0;
    }
}