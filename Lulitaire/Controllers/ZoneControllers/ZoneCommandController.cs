using System.Net;
using System.Security.Claims;
using Application.Features.Zone.Commands;
using Application.Features.Zone.Commands.Create;
using Application.Features.Zone.Commands.Delete;
using Application.Features.Zone.Commands.PatchUsername;
using Application.Services;
using Application.Shared.Exceptions;
using Application.user.commands;
using Application.user.commands.create;
using Application.user.commands.PatchUsername;
using Application.user.commands.put;
using Application.user.queries.GetByUsernameOrMailAndPassword;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lulitaire.Controllers.ZoneControllers;
[ApiController]
[Route("api/zones")]
[Tags("Zones")]

public class ZoneCommandController : ControllerBase
{
    private readonly ZoneCommandProcessor _zoneCommandProcessor;
    private readonly TokenService _service;


    public ZoneCommandController( ZoneCommandProcessor zoneCommandProcessor, TokenService service)
    {
        _zoneCommandProcessor = zoneCommandProcessor;
        _service = service;
    }
    
    [HttpPost]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<ZoneCreateOutput> Create([FromBody] ZoneCreateCommand command)
    {
        try
        {
            Request.Cookies.TryGetValue("AuthToken", out var token);
            command.user_id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var result = _zoneCommandProcessor.Create(command);

            return new OkObjectResult(result);
        }
        catch (ArgumentException e)
        {
            return BadRequest(new { message = e.Message });
        }
        catch (Exception e)
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
            string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            ZoneDeleteCommand command = new ZoneDeleteCommand
            {
                id = id,
                userId = userId
            };
            _zoneCommandProcessor.Delete(command);
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

    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult Patch([FromBody] ZonePatchCommand command,int id)
    {
        try
        {
            command.Id = id;
            command.user_id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            _zoneCommandProcessor.Patch(command);
            return NoContent();
        }
        catch (UnauthorizedAccessException e)
        {
            return Unauthorized(new {message = e.Message });
        }

    }
    // [HttpPut("{id}")]
    // [ProducesResponseType(StatusCodes.Status204NoContent)]
    // [ProducesResponseType(StatusCodes.Status404NotFound)]
    // [ProducesResponseType(StatusCodes.Status400BadRequest)]
    // public ActionResult Put(UserPutCommand command, int id)
    // {
    //     try
    //     {
    //         if (User.FindFirst(ClaimTypes.Role)?.Value != "Admin")
    //             throw new UnauthorizedAccessException();
    //         command.Id = id;
    //         _zoneCommandProcessor.Put(command);
    //         return NoContent();
    //     }
    //     catch (ArgumentException e)
    //     {
    //         return BadRequest(new { message = e.Message });
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
    //

}