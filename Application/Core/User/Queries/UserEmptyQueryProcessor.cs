using Application.user.queries.getAll;
using Application.utils;

namespace Application.user.queries;

public class UserEmptyQueryProcessor
{
    private readonly IEmptyQueryHandler<UserGetallOutput> _userGetAllHandler;
    private readonly IEmptyQueryHandler<UserGetCurrentUserOutput> _userGetCurrentUserHandler;

    public UserEmptyQueryProcessor(IEmptyQueryHandler<UserGetallOutput> userGetAllHandler, IEmptyQueryHandler<UserGetCurrentUserOutput> userGetCurrentUserHandler)
    {
        _userGetAllHandler = userGetAllHandler;
        _userGetCurrentUserHandler = userGetCurrentUserHandler;
    }

    public UserGetallOutput GetAll()
    {
        return _userGetAllHandler.Handle();
    }
    public UserGetCurrentUserOutput GetCurrentUser()
    {
        return _userGetCurrentUserHandler.Handle();
    }
}