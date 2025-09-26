using Application.user.queries.getAll;
using Application.utils;
using Infrastructure.Entities;
using Infrastructure.Repositories.Generic;

namespace Application.user.queries;

public class ZoneEmptyQueryProcessor
{
    private readonly IEmptyQueryHandler<ZoneGetallOutput> _zoneGetAllHandler;
    private readonly IGenericRepository<DbZone> _repository;
    private readonly IEmptyQueryHandler<UserGetCurrentUserOutput> _userGetCurrentUserHandler;

    public ZoneEmptyQueryProcessor(IEmptyQueryHandler<ZoneGetallOutput> zoneGetAllHandler, IEmptyQueryHandler<UserGetCurrentUserOutput> userGetCurrentUserHandler, IGenericRepository<DbZone> repository)
    {
        _zoneGetAllHandler = zoneGetAllHandler;
        _userGetCurrentUserHandler = userGetCurrentUserHandler;
        _repository = repository;
    }

    public ZoneGetallOutput GetAll(string? value = "")
    {
        return _zoneGetAllHandler.Handle();
    }
    public ZoneGetallOutput HandleForUser(string userId)
    {
        return new ZoneGetallOutput
        {
            Zones = _repository.GetAll()
                .Where(zone => zone.Id_user == int.Parse(userId))
                .Select(zone => new ZoneGetallOutput.ZoneDto
                {
                    Id = zone.Id,
                    Name = zone.Name,
                    Id_user = zone.Id_user
                })
                .ToList()
        };
    }
    // public UserGetCurrentUserOutput GetCurrentUser()
    // {
    //     return _userGetCurrentUserHandler.Handle();
    // }
}