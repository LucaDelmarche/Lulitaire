using System.Security.Claims;
using Application.Core.User;
using Application.Core.User.Queries.GetByUsernameOrMail;
using Application.Core.User.Queries.GetByUsernameOrMailAndPassword;
using Application.Features.Zone.Queries;
using Application.Features.Zone.Queries.GetAll;
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
[Route("api/zones")]
[Tags("Zones")]

public class ZoneQueryController :ControllerBase
{
    private readonly ZoneQueryProcessor _zoneQueryProcessor;
    private readonly ZoneEmptyQueryProcessor _zoneEmptyQueryProcessor;
    private readonly TokenService _service;

    public ZoneQueryController(ZoneEmptyQueryProcessor zoneEmptyQueryProcessor, ZoneQueryProcessor zoneQueryProcessor, TokenService service)
    {
        _zoneQueryProcessor = zoneQueryProcessor;
        _zoneEmptyQueryProcessor = zoneEmptyQueryProcessor;
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
            ZoneGetAllQuery query = new ZoneGetAllQuery
            {
                id_user = User.FindFirst(ClaimTypes.NameIdentifier)?.Value!
            };
            var entities = _zoneQueryProcessor.GetAllZones(query)
                .Zones
                .Skip(skip)
                .Take(pageSize)
                .ToList();
    
            var totalEntities = _zoneEmptyQueryProcessor.HandleForUser(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!).Zones.Count();
    
            var result = new 
            {
                TotalItems = totalEntities,
                PageSize = pageSize,
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling((double)totalEntities / pageSize),
                Items = entities
            };
            return new OkObjectResult(result);
        }
        catch (Exception e)
        {
            return BadRequest(new { message = e.Message });
        }
    }
    //
    // [Authorize]
    // [HttpGet("current")]
    // [ProducesResponseType(StatusCodes.Status200OK)]
    // [ProducesResponseType(StatusCodes.Status404NotFound)]
    // public ActionResult<UserGetCurrentUserOutput> GetCurrentUser()
    // {
    //     try
    //     {
    //         return new OkObjectResult(_userEmptyQueryProcessor.GetCurrentUser().User);
    //     }
    //     catch (UnauthorizedAccessException e)
    //     {
    //         return NotFound(new { message = e.Message });
    //     }
    // }
    //
    // [HttpPost("login")]
    // [ProducesResponseType(StatusCodes.Status200OK)]
    // [ProducesResponseType(StatusCodes.Status404NotFound)]
    // [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    // public ActionResult<UserGetByUsernameOrMailAndPasswordOutput.UserGetBytUsernameDto> GetByUsernameOrMail([FromBody] UserLogin credentials)
    // {
    //     string usernameOrMailAndPassword = credentials.Email;
    //     string password = credentials.Password;
    //     UserGetByUsernameOrMailAndPasswordQuery query = new UserGetByUsernameOrMailAndPasswordQuery
    //         {
    //             UsernameOrMail = usernameOrMailAndPassword,
    //             Password = password
    //         };
    //     try
    //     {
    //         UserGetByUsernameOrMailAndPasswordOutput output = new UserGetByUsernameOrMailAndPasswordOutput()
    //         {
    //                 UserGetBytUsername = _userQueryProcessor.GetByUsernameOrMailAndPassword(query).UserGetBytUsername
    //         };
    //         query.Role = output.UserGetBytUsername.Role.ToString();
    //         var token = _service.GenerateJwtToken(query, output.UserGetBytUsername.Id.ToString());
    //         Response.Cookies.Append("AuthToken", token, new CookieOptions
    //         {
    //             HttpOnly = true, 
    //             Secure = true,        
    //             SameSite = SameSiteMode.Strict, 
    //             Expires = DateTimeOffset.UtcNow.AddMinutes(30)
    //         });
    //         return new OkObjectResult(output.UserGetBytUsername);
    //     }
    //     catch (EntityNotFoundException<User> e)
    //     {
    //         return NotFound(new { message = e.Message });
    //     }
    //     catch (EntityIncorrectPasswordExceptionOrIdentifier e)
    //     {
    //         return Unauthorized(new { message = e.Message });
    //     }
    // }
    //
    // [Authorize]
    // [HttpGet("{usernameOrMail}")]
    // [ProducesResponseType(StatusCodes.Status200OK)]
    // [ProducesResponseType(StatusCodes.Status404NotFound)]
    // [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    // public ActionResult<UserGetByUsernameOrMailOutput.UserGetByUsernameDto> GetBysernameOrMail(string usernameOrMail)
    // {
    //     UserGetByUsernameOrMailQuery query = new UserGetByUsernameOrMailQuery
    //     {
    //         UsernameOrMail = usernameOrMail,
    //     };
    //     try
    //     {
    //         if (User.Identity?.Name != usernameOrMail && User.FindFirst(ClaimTypes.Role)?.Value != "1")
    //             throw new UnauthorizedAccessException();
    //         
    //         return new OkObjectResult(_userQueryProcessor.GetByUsernameOrMail(query).UserGetByUsername);
    //     }
    //     catch (EntityNotFoundException<User> e)
    //     {
    //         return NotFound(new { message = e.Message });
    //     }
    //     catch (UnauthorizedAccessException e)
    //     {
    //         return Unauthorized(new {message = e.Message });
    //     }
    // }
    

}