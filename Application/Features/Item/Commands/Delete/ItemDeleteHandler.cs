using Application.Features.Zone.Commands.Delete;
using Application.Shared.Exceptions;
using Application.utils;
using Infrastructure.Entities;
using Infrastructure.Repositories.Generic;

namespace Application.Features.Item.Commands.Delete;

public class ItemDeleteHandler : IEmptyOutputCommandHandler<ItemDeleteCommand>
{
    private readonly IGenericRepository<DbItem> _itemRepository;
    private readonly IGenericRepository<DbZone> _zoneRepository;

    public ItemDeleteHandler(IGenericRepository<DbItem> itemRepository, IGenericRepository<DbZone> zoneRepository)
    {
        _itemRepository = itemRepository;
        _zoneRepository = zoneRepository;
    }

    public void Handle(ItemDeleteCommand input)
    {
        List<DbItem?> items = _itemRepository.GetAll().ToList();
        var dbItem = _itemRepository.GetById(input.id);
        if (dbItem == null)
            throw new EntityNotFoundException<Domain.Item>(input.id);

        // Vérifie si l'utilisateur courant est autorisé
        if (_zoneRepository.GetById(dbItem.ZoneId)!.Id_user != int.Parse(input.userId!))
            throw new UnauthorizedAccessException("You are not authorized to delete this zone.");

        // Supprime directement l'entité existante
        _itemRepository.Delete(dbItem);
    }
}