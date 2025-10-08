using Application.user.queries.getAll;
using Application.utils;
using Infrastructure.Entities;
using Infrastructure.Repositories.Generic;

namespace Application.Features.Item.Queries;

public class ItemEmptyQueryProcessor
{
    private readonly IEmptyQueryHandler<ItemGetallOutput> _itemGetAllHandler;
    private readonly IGenericRepository<DbItem> _repository;
    private readonly IEmptyQueryHandler<UserGetCurrentUserOutput> _userGetCurrentUserHandler;

    public ItemEmptyQueryProcessor(IEmptyQueryHandler<ItemGetallOutput> itemGetAllHandler, IEmptyQueryHandler<UserGetCurrentUserOutput> userGetCurrentUserHandler, IGenericRepository<DbItem> repository)
    {
        _itemGetAllHandler = itemGetAllHandler;
        _userGetCurrentUserHandler = userGetCurrentUserHandler;
        _repository = repository;
    }

    public ItemGetallOutput GetAll(string? value = "")
    {
        return _itemGetAllHandler.Handle();
    }
    public ItemGetallOutput HandleForUser(string userId)
    {
        return new ItemGetallOutput
        {
            Items = _repository.GetAll()
                .Where(item => item.UserId == int.Parse(userId))
                .Select(item => new ItemGetallOutput.ItemDto
                {
                    Id = item.Id,
                    Name = item.Name,
                    Id_user = item.UserId,
                    Quantity = item.Quantity,
                    Unit = item.Unit,
                    ExpirationDate = item.ExpritationData,
                })
                .ToList()
        };
    }
    // public UserGetCurrentUserOutput GetCurrentUser()
    // {
    //     return _userGetCurrentUserHandler.Handle();
    // }
}