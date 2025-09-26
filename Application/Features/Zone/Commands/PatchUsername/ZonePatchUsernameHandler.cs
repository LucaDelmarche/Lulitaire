using Application.Shared.Exceptions;
using Application.user.commands.PatchUsername;
using Application.utils;
using AutoMapper;
using Domain;
using Infrastructure.Entities;
using Infrastructure.Repositories.Generic;

namespace Application.Features.Zone.Commands.PatchUsername;

public class ZonePatchUsernameHandler: IEmptyOutputCommandHandler<ZonePatchCommand>
{
    private readonly IGenericRepository<DbZone> _repository;
    private readonly IMapper _iMapper;

    public ZonePatchUsernameHandler(IGenericRepository<DbZone> repository, IMapper iMapper)
    {
        _repository = repository;
        _iMapper = iMapper;
    }
    public void Handle(ZonePatchCommand command)
    {
        DbZone? entity = _repository.GetById(command.Id);

        if (entity == null)
            throw new EntityNotFoundException<User>(command.Id);
        if(command.user_id != _repository.GetById(command.Id)?.Id_user.ToString())
            throw new UnauthorizedAccessException("You are not authorized to modify this zone.");
        Domain.Zone zone = _iMapper.Map<Domain.Zone>(entity);
        zone.Name = command.Name;
        
        _iMapper.Map(zone, entity);
        _repository.Save(entity); 
    }
}