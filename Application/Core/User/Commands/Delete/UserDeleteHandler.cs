using Application.Shared.Exceptions;
using Application.utils;
using Domain;
using Infrastructure;
using Infrastructure.Entities;
using Infrastructure.Repositories.Generic;
using Infrastructure.Repository;

namespace Application.user.commands.delete;

public class UserDeleteHandler: IEmptyOutputCommandHandler<int>
{
    private readonly IGenericRepository<DbUser> _userRepository;

    public UserDeleteHandler(IGenericRepository<DbUser> userRepository)
    {
        _userRepository = userRepository;
    }

    public void Handle(int id)
    {
        if(_userRepository.ExistsById(id))
            _userRepository.DeleteById(id);
        else
            throw new EntityNotFoundException<User>(id);
    }
}