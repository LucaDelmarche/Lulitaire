using Application.Services;
using Application.Shared.Exceptions;
using Application.utils;
using AutoMapper;
using Domain;
using Infrastructure;
using Infrastructure.Entities;
using Infrastructure.Repositories.Generic;
using Infrastructure.Repository;

namespace Application.user.commands.create;

public class UserCreateHandler : ICommandHandler<UserCreateCommand, UserCreateOutput.UserCreateDto>
{
    private readonly IGenericRepository<DbUser> _userRepository;
    private readonly IMapper _iMapper;

    public UserCreateHandler(IGenericRepository<DbUser> userRepository, IMapper modelMapper)
    {
        _userRepository = userRepository;
        _iMapper = modelMapper;
    }
    
    public UserCreateOutput.UserCreateDto Handle(UserCreateCommand command)
    {
        IEnumerable<DbUser?> allUsers = _userRepository.GetAll();
        if (allUsers.Any(u => u?.Mail == command.Mail))
        {
            throw new AlreadyExistsException<User>(command.Mail);
        }
        string username = RandomStringGenerator.RandomString(15);
        User user = new User(username,command.Mail,command.Password,0);
        DbUser? dbUser = _iMapper.Map<DbUser>(user);
        
        DbUser? dbUserCreated = _userRepository.Add(dbUser);
        
        return _iMapper.Map<UserCreateOutput.UserCreateDto>(dbUserCreated);
    }
}