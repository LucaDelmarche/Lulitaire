using Application.Core.User.Queries.GetById;
using Application.Core.User.Queries.GetByUsernameOrMail;
using Application.Core.User.Queries.GetByUsernameOrMailAndPassword;
using Application.Features.Zone.Queries.GetAll;
using Application.user.queries.getAll;
using Application.user.queries.GetByUsernameOrMailAndPassword;
using Application.utils;

namespace Application.Features.Zone.Queries;

public class ZoneQueryProcessor
{
    private readonly IQueryHandler<ZoneGetAllQuery,ZoneGetallOutput> _zoneGetAllHandler;
    private readonly IQueryHandler<UserGetByUsernameOrMailQuery,UserGetByUsernameOrMailOutput> _userGetBytUsernameOrMailHandler;
    private readonly IQueryHandler<UserGetByIdQuery,UserGetByIdOutput> _userGetByIdHandler;
    public ZoneQueryProcessor(IQueryHandler<ZoneGetAllQuery,ZoneGetallOutput>  zoneGetAllHandler, IQueryHandler<UserGetByUsernameOrMailQuery, UserGetByUsernameOrMailOutput> userGetBytUsernameOrMailHandler, IQueryHandler<UserGetByIdQuery, UserGetByIdOutput> userGetByIdHandler)
    {
        _zoneGetAllHandler = zoneGetAllHandler;
        _userGetBytUsernameOrMailHandler = userGetBytUsernameOrMailHandler;
        _userGetByIdHandler = userGetByIdHandler;
    }

    public ZoneGetallOutput GetAllZones(ZoneGetAllQuery query)
    {
        return _zoneGetAllHandler.Handle(query);
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