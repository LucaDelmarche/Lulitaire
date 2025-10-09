using System.Security.Claims;
using Application.Features.Item.Queries;
using Application.Features.Item.Queries.GetAll;
using Application.Features.Zone.Queries;
using Application.Features.Zone.Queries.GetAll;
using Application.Services;
using Application.user.queries;
using Application.user.queries.getAll;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lulitaire.Controllers.ItemControllers;
[ApiController]
[Route("api/items")]
[Tags("Items")]

public class ItemQueryController :ControllerBase
{
    private readonly ItemQueryProcessor _itemQueryProcessor;
    private readonly ItemEmptyQueryProcessor _itemEmptyQueryProcessor;
    private readonly TokenService _service;

    public ItemQueryController(ItemEmptyQueryProcessor itemEmptyQueryProcessor, ItemQueryProcessor itemQueryProcessor, TokenService service)
    {
        _itemQueryProcessor = itemQueryProcessor;
        _itemEmptyQueryProcessor = itemEmptyQueryProcessor;
        _service = service;
    }
    
    [Authorize]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<ItemGetallOutput> GetAll(int page, int pageSize, int idZone)
    {
        try
        {
            var skip = (page - 1) * pageSize;
            ItemGetAllQuery query = new ItemGetAllQuery
            {
                id_user = User.FindFirst(ClaimTypes.NameIdentifier)?.Value!,
                id_zone = idZone
            };
            var entities = _itemQueryProcessor.GetAllItems(query)
                .Items
                .Skip(skip)
                .Take(pageSize)
                .ToList();
    
            var totalEntities = _itemEmptyQueryProcessor.HandleForUser(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!).Items.Count();
    
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