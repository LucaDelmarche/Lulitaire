using Application.utils;
using AutoMapper;
using Infrastructure;
using Infrastructure.Entities;
using Infrastructure.Repositories.Generic;
using Infrastructure.Repository;

namespace Application.user.queries.getAll;

public class UserGetAllHandler : IEmptyQueryHandler<UserGetallOutput>
{
    private readonly IGenericRepository<DbUser> _repository;
    private readonly IMapper _iMapper;

    public UserGetAllHandler(IGenericRepository<DbUser> repository, IMapper iMapper)
    {
        _repository = repository;
        _iMapper = iMapper;
    }
    
    public UserGetallOutput Handle()
    {
        IEnumerable<DbUser> users = _repository.GetAll();
        UserGetallOutput output = new UserGetallOutput
        {
            Users = new List<UserGetallOutput.UserDto>()
        };
        foreach (DbUser? user in users)
        {
            UserGetallOutput.UserDto userDto = new UserGetallOutput.UserDto
            {
                Id = user.Id,
                Username = user.Username,
                Mail = user.Mail,
                Role = user.Role,
            };
            output.Users.Add(userDto);
        }

        return output;
    }

}