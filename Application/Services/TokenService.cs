using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.user.queries.GetByUsernameOrMailAndPassword;
using Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services;

public class TokenService
{
    private const double EXPIRY_DURATION_MINUTES = 1440;
    private readonly IConfiguration _configuration;
    private JwtSecurityToken _token;

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateJwtToken(UserGetByUsernameOrMailAndPasswordQuery user,string id)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, user.UsernameOrMail),
            new Claim(ClaimTypes.Role, user.Role),
            new Claim(ClaimTypes.NameIdentifier, id)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        _token = new JwtSecurityToken(
            claims:claims,
            expires: DateTime.Now.AddMinutes(EXPIRY_DURATION_MINUTES),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(_token);
    }

    public bool IsTokenValid(string key, string issuer, string token)
    {
        var mySecret = Encoding.UTF8.GetBytes(key);
        var mySecurityKey = new SymmetricSecurityKey(mySecret);
        var tokenHandler = new JwtSecurityTokenHandler();
        try
        {
            tokenHandler.ValidateToken(token,
                new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = issuer,
                    ValidAudience = issuer,
                    IssuerSigningKey = mySecurityKey,
                }, out SecurityToken validatedToken);
        }
        catch
        {
            return false;
        }

        return true;
    }
    public JwtSecurityToken GetTokenFromAuthorizationHeader(string authorizationHeader)
    {
        if (string.IsNullOrEmpty(authorizationHeader))
            return null;

        var token = authorizationHeader.Replace("Bearer ", "");
        var tokenHandler = new JwtSecurityTokenHandler();

        return tokenHandler.ReadToken(token) as JwtSecurityToken;
    }
    public JwtSecurityToken GetToken()
    {
        return _token;
    }
}