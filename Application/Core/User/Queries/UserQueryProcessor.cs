using Application.Core.User.Queries.GetById;
using Application.Core.User.Queries.GetByUsernameOrMail;
using Application.Core.User.Queries.GetByUsernameOrMailAndPassword;
using Application.user.queries.GetByUsernameOrMailAndPassword;
using Application.utils;

namespace Application.Core.User;

public class UserQueryProcessor
{
    private readonly IQueryHandler<UserGetByUsernameOrMailAndPasswordQuery,UserGetByUsernameOrMailAndPasswordOutput> _userGetBytUsernameOrMailAndPasswordHandler;
    private readonly IQueryHandler<UserGetByUsernameOrMailQuery,UserGetByUsernameOrMailOutput> _userGetBytUsernameOrMailHandler;
    private readonly IQueryHandler<UserGetByIdQuery,UserGetByIdOutput> _userGetByIdHandler;
    public UserQueryProcessor(IQueryHandler<UserGetByUsernameOrMailAndPasswordQuery,UserGetByUsernameOrMailAndPasswordOutput>  userGetBytUsernameOrMailAndPasswordHandler, IQueryHandler<UserGetByUsernameOrMailQuery, UserGetByUsernameOrMailOutput> userGetBytUsernameOrMailHandler, IQueryHandler<UserGetByIdQuery, UserGetByIdOutput> userGetByIdHandler)
    {
        _userGetBytUsernameOrMailAndPasswordHandler = userGetBytUsernameOrMailAndPasswordHandler;
        _userGetBytUsernameOrMailHandler = userGetBytUsernameOrMailHandler;
        _userGetByIdHandler = userGetByIdHandler;
    }

    public UserGetByUsernameOrMailAndPasswordOutput GetByUsernameOrMailAndPassword(UserGetByUsernameOrMailAndPasswordQuery query)
    {
        return _userGetBytUsernameOrMailAndPasswordHandler.Handle(query);
    }
    
    public UserGetByUsernameOrMailOutput GetByUsernameOrMail(UserGetByUsernameOrMailQuery query)
    {
        return _userGetBytUsernameOrMailHandler.Handle(query);
    }

    public UserGetByIdOutput GetById(UserGetByIdQuery query)
    {
        return _userGetByIdHandler.Handle(query);
    }
}