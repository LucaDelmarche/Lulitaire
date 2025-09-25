using System.Security.Claims;
using Application.Core.User;
using Application.Core.User.Queries.GetByUsernameOrMail;
using Application.Core.User.Queries.GetByUsernameOrMailAndPassword;
using Application.Services;
using Application.Shared.Exceptions;
using Application.user.queries;
using Application.user.queries.getAll;
using Application.user.queries.GetByUsernameOrMailAndPassword;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lulitaire.Controllers.UserControllers;
[ApiController]
[Route("api/users")]
[Tags("Users")]

public class UserQueryController :ControllerBase
{
    private readonly UserEmptyQueryProcessor _userEmptyQueryProcessor;
    private readonly UserQueryProcessor _userQueryProcessor;
    private readonly TokenService _service;

    public UserQueryController(UserQueryProcessor userQueryProcessor, UserEmptyQueryProcessor userEmptyQueryProcessor, TokenService service)
    {
        _userQueryProcessor = userQueryProcessor;
        _userEmptyQueryProcessor = userEmptyQueryProcessor;
        _service = service;
    }
    
    [Authorize]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<UserGetallOutput> GetAll(int page, int pageSize)
    {
        try
        {
            var skip = (page - 1) * pageSize;

            var entities = _userEmptyQueryProcessor.GetAll()
                .Users
                .Skip(skip)
                .Take(pageSize)
                .ToList();

            var totalEntities = _userEmptyQueryProcessor.GetAll().Users.Count();

            var result = new 
            {
                TotalItems = totalEntities,
                PageSize = pageSize,
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling((double)totalEntities / pageSize),
                Items = entities
            };
            if (User.FindFirst(ClaimTypes.Role)?.Value != "Admin")
                throw new UnauthorizedAccessException();
            return new OkObjectResult(result);
        }
        catch (UnauthorizedAccessException e)
        {
            return Unauthorized(new { message = e.Message });
        }
    }
    
    [Authorize]
    [HttpGet("current")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<UserGetCurrentUserOutput> GetCurrentUser()
    {
        try
        {
            return new OkObjectResult(_userEmptyQueryProcessor.GetCurrentUser().User);
        }
        catch (UnauthorizedAccessException e)
        {
            return NotFound(new { message = e.Message });
        }
    }
    
    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public ActionResult<UserGetByUsernameOrMailAndPasswordOutput.UserGetBytUsernameDto> GetByUsernameOrMail([FromBody] UserLogin credentials)
    {
        string usernameOrMailAndPassword = credentials.Email;
        string password = credentials.Password;
        UserGetByUsernameOrMailAndPasswordQuery query = new UserGetByUsernameOrMailAndPasswordQuery
            {
                UsernameOrMail = usernameOrMailAndPassword,
                Password = password
            };
        try
        {
            UserGetByUsernameOrMailAndPasswordOutput output = new UserGetByUsernameOrMailAndPasswordOutput()
            {
                    UserGetBytUsername = _userQueryProcessor.GetByUsernameOrMailAndPassword(query).UserGetBytUsername
            };
            query.Role = output.UserGetBytUsername.Role.ToString();
            var token = _service.GenerateJwtToken(query, output.UserGetBytUsername.Id.ToString());
            Response.Cookies.Append("AuthToken", token, new CookieOptions
            {
                HttpOnly = true, 
                Secure = true,        
                SameSite = SameSiteMode.Strict, 
                Expires = DateTimeOffset.UtcNow.AddMinutes(30)
            });
            return new OkObjectResult(output.UserGetBytUsername);
        }
        catch (EntityNotFoundException<User> e)
        {
            return NotFound(new { message = e.Message });
        }
        catch (EntityIncorrectPasswordExceptionOrIdentifier e)
        {
            return Unauthorized(new { message = e.Message });
        }
    }
    
    [Authorize]
    [HttpGet("{usernameOrMail}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public ActionResult<UserGetByUsernameOrMailOutput.UserGetByUsernameDto> GetBysernameOrMail(string usernameOrMail)
    {
        UserGetByUsernameOrMailQuery query = new UserGetByUsernameOrMailQuery
        {
            UsernameOrMail = usernameOrMail,
        };
        try
        {
            if (User.Identity?.Name != usernameOrMail && User.FindFirst(ClaimTypes.Role)?.Value != "1")
                throw new UnauthorizedAccessException();
            
            return new OkObjectResult(_userQueryProcessor.GetByUsernameOrMail(query).UserGetByUsername);
        }
        catch (EntityNotFoundException<User> e)
        {
            return NotFound(new { message = e.Message });
        }
        catch (UnauthorizedAccessException e)
        {
            return Unauthorized(new {message = e.Message });
        }
    }
    

}