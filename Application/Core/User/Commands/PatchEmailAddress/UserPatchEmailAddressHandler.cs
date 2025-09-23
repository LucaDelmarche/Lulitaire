using Application.Shared.Exceptions;
using Application.user.commands.PatchUsername;
using Application.utils;
using AutoMapper;
using Infrastructure;
using Infrastructure.Entities;
using Infrastructure.Repositories.Generic;
using Infrastructure.Repository;

namespace Application.Core.User.Commands.PatchEmailAddress;

public class UserPatchEmailAddressHandler: IEmptyOutputCommandHandler<UserPatchEmailAddressCommand>
{
    private readonly IGenericRepository<DbUser> _repository;
    private readonly IMapper _iMapper;

    public UserPatchEmailAddressHandler(IGenericRepository<DbUser> repository, IMapper iMapper)
    {
        _repository = repository;
        _iMapper = iMapper;
    }
    public void Handle(UserPatchEmailAddressCommand command)
    {
        DbUser? entity = _repository.GetById(command.Id);

        if (entity == null)
            throw new EntityNotFoundException<Domain.User>(command.Id);

        Domain.User user = _iMapper.Map<Domain.User>(entity);
        user.Mail = command.Email;
        
        _iMapper.Map(user, entity);
        _repository.Save(entity); 
    }
}