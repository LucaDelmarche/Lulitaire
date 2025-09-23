using Application.Shared.Exceptions;
using Application.user.queries.GetByUsernameOrMailAndPassword;
using Application.utils;
using AutoMapper;
using Infrastructure;
using Infrastructure.Entities;
using Infrastructure.Repositories.User;
using Infrastructure.Repository;

namespace Application.Core.User.Queries.GetByUsernameOrMail;

public class UserGetByUsernameOrMailHandler: IQueryHandler<UserGetByUsernameOrMailQuery, UserGetByUsernameOrMailOutput>
{
    private readonly IUserRepository _repository;
    private readonly IMapper _iMapper;

    public UserGetByUsernameOrMailHandler(IUserRepository repository, IMapper iMapper)
    {
        _repository = repository;
        _iMapper = iMapper;
    }
    
    public UserGetByUsernameOrMailOutput Handle(UserGetByUsernameOrMailQuery input)
    {
        DbUser? dbUser = _repository.GetByUsernameOrEmail(input.UsernameOrMail);
        if(dbUser == null)
            throw new EntityNotFoundException<Domain.User>(input.UsernameOrMail);
        
        UserGetByUsernameOrMailOutput output = new UserGetByUsernameOrMailOutput();
        output.UserGetByUsername = _iMapper.Map<UserGetByUsernameOrMailOutput.UserGetByUsernameDto>(dbUser);
        return output;
    }
}