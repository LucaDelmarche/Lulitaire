using System.Net;
using System.Security.Claims;
using Application.Features.Item.Commands;
using Application.Features.Item.Commands.Create;
using Application.Features.Item.Commands.Patch;
using Application.Features.Zone.Commands;
using Application.Features.Zone.Commands.Create;
using Application.Features.Zone.Commands.Delete;
using Application.Features.Zone.Commands.PatchUsername;
using Application.Services;
using Application.Shared.Exceptions;
using Application.user.commands.create;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lulitaire.Controllers.ItemControllers;
[ApiController]
[Route("api/items")]
[Tags("Items")]

public class ItemCommandController : ControllerBase
{
    private readonly ItemCommandProcessor _itemCommandProcessor;


    public ItemCommandController( ItemCommandProcessor itemCommandProcessor)
    {
        _itemCommandProcessor = itemCommandProcessor;
    }
    
    [HttpPost]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<ItemCreateOutput> Create([FromBody] ItemCreateCommand command)
    {
        try
        {
            Request.Cookies.TryGetValue("AuthToken", out var token);
            command.user_id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var result = _itemCommandProcessor.Create(command);

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
            ItemDeleteCommand command = new ItemDeleteCommand
            {
                id = id,
                userId = userId
            };
            _itemCommandProcessor.Delete(command);
            return NoContent();
        }
        catch (EntityNotFoundException<Item> e)
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
    public ActionResult Patch([FromBody] ItemPatchCommand command,int id)
    {
        try
        {
            command.Id = id;
            command.user_id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            _itemCommandProcessor.Patch(command);
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