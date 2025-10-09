using Application.Features.Zone.Commands.PatchUsername;
using Application.Shared.Exceptions;
using Application.utils;
using AutoMapper;
using Domain;
using Infrastructure.Entities;
using Infrastructure.Repositories.Generic;

namespace Application.Features.Item.Commands.Patch;

public class ItemPatchHandler: IEmptyOutputCommandHandler<ItemPatchCommand>
{
    private readonly IGenericRepository<DbItem> _repository;
    private readonly IMapper _iMapper;

    public ItemPatchHandler(IGenericRepository<DbItem> repository, IMapper iMapper)
    {
        _repository = repository;
        _iMapper = iMapper;
    }
    public void Handle(ItemPatchCommand command)
    {
        DbItem? entity = _repository.GetById(command.Id);

        if (entity == null)
            throw new EntityNotFoundException<Domain.Item>(command.Id);
        if(command.user_id != _repository.GetById(command.Id)?.UserId.ToString())
            throw new UnauthorizedAccessException("You are not authorized to modify this zone.");
        Domain.Item item = _iMapper.Map<Domain.Item>(entity);
        _iMapper.Map(command, item);
        _iMapper.Map(item, entity);
        _repository.Save(entity); 
    }
}