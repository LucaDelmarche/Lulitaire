using Application.Shared.Exceptions;
using Application.user.commands.put;
using Application.utils;
using AutoMapper;
using Domain;
using Infrastructure;
using Infrastructure.Entities;
using Infrastructure.Repositories.Generic;
using Infrastructure.Repository;

namespace Application.user.commands.PatchUsername;

public class UserPatchUsernameHandler: IEmptyOutputCommandHandler<UserPatchUsernameCommand>
{
    private readonly IGenericRepository<DbUser> _repository;
    private readonly IMapper _iMapper;

    public UserPatchUsernameHandler(IGenericRepository<DbUser> repository, IMapper iMapper)
    {
        _repository = repository;
        _iMapper = iMapper;
    }
    public void Handle(UserPatchUsernameCommand command)
    {
        DbUser? entity = _repository.GetById(command.Id);

        if (entity == null)
            throw new EntityNotFoundException<User>(command.Id);

        User user = _iMapper.Map<User>(entity);
        user.Username = command.Username;
        
        _iMapper.Map(user, entity);
        _repository.Save(entity); 
    }
}