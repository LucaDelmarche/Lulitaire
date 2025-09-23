using System.Text;
using Application.Shared.Exceptions;
using Application.utils;
using AutoMapper;
using Domain;
using Infrastructure;
using Infrastructure.Entities;
using Infrastructure.Repositories.Generic;
using Infrastructure.Repository;

namespace Application.user.commands.put;

public class UserPutHandler : IEmptyOutputCommandHandler<UserPutCommand>
{
    private readonly IGenericRepository<DbUser> _repository;
    private readonly IMapper _iMapper;

    public UserPutHandler(IGenericRepository<DbUser> repository, IMapper iMapper)
    {
        _repository = repository;
        _iMapper = iMapper;
    }
    public void Handle(UserPutCommand command)
    {
        DbUser? entity = _repository.GetById(command.Id);

        if (entity == null)
            throw new EntityNotFoundException<User>(command.Id);     
        
        User user = new User(command.Username,command.Mail,
            command.Password,
            command.Role);
        
        _iMapper.Map(user, entity);
        _repository.Save(entity); 
    }
}