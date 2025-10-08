using Application.Core.User.Queries.GetById;
using Application.Core.User.Queries.GetByUsernameOrMail;
using Application.Features.Item.Queries.GetAll;
using Application.Features.Zone.Queries.GetAll;
using Application.user.queries.getAll;
using Application.user.queries.GetByUsernameOrMailAndPassword;
using Application.utils;

namespace Application.Features.Item.Queries;

public class ItemQueryProcessor
{
    private readonly IQueryHandler<ItemGetAllQuery,ItemGetallOutput> _itemGetAllHandler;
    private readonly IQueryHandler<UserGetByUsernameOrMailQuery,UserGetByUsernameOrMailOutput> _userGetBytUsernameOrMailHandler;
    private readonly IQueryHandler<UserGetByIdQuery,UserGetByIdOutput> _userGetByIdHandler;
    public ItemQueryProcessor(IQueryHandler<ItemGetAllQuery,ItemGetallOutput>  itemGetAllHandler, IQueryHandler<UserGetByUsernameOrMailQuery, UserGetByUsernameOrMailOutput> userGetBytUsernameOrMailHandler, IQueryHandler<UserGetByIdQuery, UserGetByIdOutput> userGetByIdHandler)
    {
        _itemGetAllHandler = itemGetAllHandler;
        _userGetBytUsernameOrMailHandler = userGetBytUsernameOrMailHandler;
        _userGetByIdHandler = userGetByIdHandler;
    }

    public ItemGetallOutput GetAllItems(ItemGetAllQuery query)
    {
        return _itemGetAllHandler.Handle(query);
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