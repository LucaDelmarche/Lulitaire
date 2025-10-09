using Application.Features.Zone.Commands.Create;
using Application.Shared.Exceptions;
using Application.user.commands.create;
using Application.utils;
using AutoMapper;
using Infrastructure.Entities;
using Infrastructure.Repositories.Generic;

namespace Application.Features.Item.Commands.Create;

public class ItemCreateHandler : ICommandHandler<ItemCreateCommand, ItemCreateOutput.ItemCreateDto>
{
    private readonly IGenericRepository<DbItem> _itemRepository;
    private readonly IMapper _iMapper;

    public ItemCreateHandler(IGenericRepository<DbItem> userRepository, IMapper modelMapper)
    {
        _itemRepository = userRepository;
        _iMapper = modelMapper;
    }
    
    public ItemCreateOutput.ItemCreateDto Handle(ItemCreateCommand command)
    {
        int userId = int.Parse(command.user_id!);
        var item = new Domain.Item(command.Name, command.Quantity, command.Unit, command.ExpirationDate, command.Location);
        // Crée l'entité
        var dbItem = new DbItem
        {
            Id=0,
            Name = command.Name,
            UserId = userId,
            Quantity = command.Quantity,
            Unit = command.Unit,
            ExpirationDate = command.ExpirationDate,
            ZoneId = command.ZoneId,
            Location = command.Location
        };

        // Ajoute et retourne la zone créée
        DbItem? dbItemCreated = _itemRepository.Add(dbItem);
        return _iMapper.Map<ItemCreateOutput.ItemCreateDto>(dbItemCreated);
    }

}