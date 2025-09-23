using System.Text;
using Application.Core.User.Queries.GetByUsernameOrMailAndPassword;
using Application.Shared.Exceptions;
using Application.utils;
using AutoMapper;
using Domain;
using Infrastructure;
using Infrastructure.Entities;
using Infrastructure.Repositories.User;
using Infrastructure.Repository;

namespace Application.user.queries.GetByUsernameOrMailAndPassword;

public class UserGetByUsernameOrMailAndPasswordHandler: IQueryHandler<UserGetByUsernameOrMailAndPasswordQuery, UserGetByUsernameOrMailAndPasswordOutput>
{
    private readonly IUserRepository _repository;
    private readonly IMapper _iMapper;

    public UserGetByUsernameOrMailAndPasswordHandler(IUserRepository repository, IMapper iMapper)
    {
        _repository = repository;
        _iMapper = iMapper;
    }
    

    public UserGetByUsernameOrMailAndPasswordOutput Handle(
        UserGetByUsernameOrMailAndPasswordQuery input)
    {
        DbUser? dbUser = _repository.GetByUsernameOrEmail(input.UsernameOrMail);
        if(dbUser == null)
            throw new EntityIncorrectPasswordExceptionOrIdentifier();

        if (!BCrypt.Net.BCrypt.Verify(input.Password,  Encoding.UTF8.GetString(dbUser.Password)))
            throw new EntityIncorrectPasswordExceptionOrIdentifier();
        
        UserGetByUsernameOrMailAndPasswordOutput output = new UserGetByUsernameOrMailAndPasswordOutput();
        output.UserGetBytUsername = _iMapper.Map<UserGetByUsernameOrMailAndPasswordOutput.UserGetBytUsernameDto>(dbUser);
        return output;

    }
}