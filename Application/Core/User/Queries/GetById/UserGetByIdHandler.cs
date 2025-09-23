using Application.Core.User.Queries.GetById;
using Application.Shared.Exceptions;
using Application.utils;
using AutoMapper;
using Infrastructure;
using Infrastructure.Entities;
using Infrastructure.Repositories.User;
using Infrastructure.Repository;

namespace Application.Core.User.Queries.GetByUsernameOrMail;

public class UserGetByIdHandler: IQueryHandler<UserGetByIdQuery, UserGetByIdOutput>
{
    private readonly IUserRepository _repository;
    private readonly IMapper _iMapper;

    public UserGetByIdHandler(IUserRepository repository, IMapper iMapper)
    {
        _repository = repository;
        _iMapper = iMapper;
    }
    
    public UserGetByIdOutput Handle(UserGetByIdQuery input)
    {
        DbUser? dbUser = _repository.GetById(input.id);
        if(dbUser == null)
            throw new EntityNotFoundException<Domain.User>(input.id);
        
        UserGetByIdOutput output = new UserGetByIdOutput();
        output.UserGetById = _iMapper.Map<UserGetByIdOutput.UserGetByIdDto>(dbUser);
        return output;
    }
}