using System.Security.Claims;
using Application.Services;
using Application.Shared.Exceptions;
using Application.utils;
using AutoMapper;
using Domain;
using Infrastructure;
using Infrastructure.Entities;
using Infrastructure.Repositories.User;
using Infrastructure.Repository;
using Microsoft.AspNetCore.Http;

namespace Application.user.queries.getAll;

public class UserGetCurrentUserHandler : IEmptyQueryHandler<UserGetCurrentUserOutput>
{
    private readonly IUserRepository _repository;
    private readonly IMapper _iMapper;
    private readonly TokenService _tokenService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserGetCurrentUserHandler(IUserRepository repository, IMapper iMapper, TokenService tokenService, IHttpContextAccessor httpContextAccessor)
    {
        _repository = repository;
        _iMapper = iMapper;
        _tokenService = tokenService;
        _httpContextAccessor = httpContextAccessor;
    }
    
    public UserGetCurrentUserOutput Handle()
    {
        var authorizationHeader = _httpContextAccessor.HttpContext?.Request.Cookies["AuthToken"];
        var token = _tokenService.GetTokenFromAuthorizationHeader(authorizationHeader);
        if (token == null)
            throw new UnauthorizedAccessException("Token is invalid or missing");
        var value = token.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        if (value == null)
            throw new EntityNotFoundException<User>(-1);
        
        int id = int.Parse(value);
        DbUser? dbUser = _repository.GetById(id);
        UserGetCurrentUserOutput output = new UserGetCurrentUserOutput
        {
            User = _iMapper.Map<UserGetCurrentUserOutput.CurrentUserDto>(dbUser)
        };
        return output;
    }
}