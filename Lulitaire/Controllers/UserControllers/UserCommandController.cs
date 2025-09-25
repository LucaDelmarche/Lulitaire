using System.Net;
using System.Security.Claims;
using Application.Services;
using Application.Shared.Exceptions;
using Application.user.commands;
using Application.user.commands.create;
using Application.user.commands.PatchUsername;
using Application.user.commands.put;
using Application.user.queries.GetByUsernameOrMailAndPassword;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace Lulitaire.Controllers.UserControllers;
[ApiController]
[Route("api/users")]
[Tags("Users")]

public class UserCommandController : ControllerBase
{
    private readonly UserCommandProcessor _userCommandProcessor;
    private readonly TokenService _service;


    public UserCommandController( UserCommandProcessor userCommandProcessor, TokenService service)
    {
        _userCommandProcessor = userCommandProcessor;
        _service = service;
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<UserCreateOutput> Create([FromBody] UserCreateCommand command)
    {
        try
        {
            var result = _userCommandProcessor.Create(command);

            return new OkObjectResult(result);
        }
        catch (ArgumentException e)
        {
            return BadRequest(new { message = e.Message });
        }
        catch (AlreadyExistsException<User> e)
        {
            return Conflict(new {message = e.Message});
        }
    }

    
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<HttpStatusCode> Delete(int id)
    {
        try
        {
            if (User.FindFirst(ClaimTypes.NameIdentifier)?.Value != id.ToString() && User.FindFirst(ClaimTypes.Role)?.Value != "Admin")
                throw new UnauthorizedAccessException();
            _userCommandProcessor.Delete(id);
            return NoContent();
        }
        catch (EntityNotFoundException<User> e)
        {
            return NotFound(new {message = e.Message });
        }
        catch (UnauthorizedAccessException e)
        {
            return Unauthorized(new {message = e.Message });
        }
    }
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult Put(UserPutCommand command, int id)
    {
        try
        {
            if (User.FindFirst(ClaimTypes.Role)?.Value != "Admin")
                throw new UnauthorizedAccessException();
            command.Id = id;
            _userCommandProcessor.Put(command);
            return NoContent();
        }
        catch (ArgumentException e)
        {
            return BadRequest(new { message = e.Message });
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
    
    [HttpPatch("{id}/username")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult Patch(int id,[FromBody] UserPatchUsernameCommand command)
    {
        try
        {
            if (User.FindFirst(ClaimTypes.NameIdentifier)?.Value != id.ToString() && User.FindFirst(ClaimTypes.Role)?.Value != "Admin")
                    throw new UnauthorizedAccessException();
            command.Id = id;
            _userCommandProcessor.PatchUsername(command);
            UserGetByUsernameOrMailAndPasswordQuery query = new UserGetByUsernameOrMailAndPasswordQuery()
            {
                Role = User.FindFirst(ClaimTypes.Role).Value,
                UsernameOrMail = command.Username,
                Password = ""
                
            };
            var token = _service.GenerateJwtToken(query, command.Id.ToString());
            Response.Cookies.Append("AuthToken", token, new CookieOptions
            {
                HttpOnly = true, 
                Secure = true,        
                SameSite = SameSiteMode.Strict, 
                Expires = DateTimeOffset.UtcNow.AddMinutes(30)
            });
            return NoContent();
        }
        catch (ArgumentException e)
        {
            return BadRequest(new { message = e.Message });
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
    [HttpPatch("{id}/email")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult Patch(int id,[FromBody] UserPatchEmailAddressCommand command)
    {
        try
        {
            if (User.FindFirst(ClaimTypes.NameIdentifier)?.Value != id.ToString() && User.FindFirst(ClaimTypes.Role)?.Value != "Admin")
                throw new UnauthorizedAccessException();
            command.Id = id;
            _userCommandProcessor.PatchEmail(command);
            return NoContent();
        }
        catch (ArgumentException e)
        {
            return BadRequest(new { message = e.Message });
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
    
    [HttpPatch("{id}/role")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult PatchRole(int id)
    {
        try
        {
            if (User.FindFirst(ClaimTypes.Role)?.Value != "Admin")
                throw new UnauthorizedAccessException();
            UserPatchRoleCommand command = new UserPatchRoleCommand
            {
                Id = id
            };
            _userCommandProcessor.PatchRole(command);
            return NoContent();
        }
        catch (ArgumentException e)
        {
            return BadRequest(new { message = e.Message });
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
    
    [HttpDelete("logout")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public ActionResult<HttpStatusCode> DeleteToken()
    {
        Response.Cookies.Delete("AuthToken"); 
        return NoContent();
    }

}