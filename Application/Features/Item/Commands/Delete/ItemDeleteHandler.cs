using Application.Features.Zone.Commands.Delete;
using Application.Shared.Exceptions;
using Application.utils;
using Infrastructure.Entities;
using Infrastructure.Repositories.Generic;

public class ItemDeleteHandler : IEmptyOutputCommandHandler<ZoneDeleteCommand>
{
    private readonly IGenericRepository<DbZone> _zoneRepository;

    public ItemDeleteHandler(IGenericRepository<DbZone> zoneRepository)
    {
        _zoneRepository = zoneRepository;
    }

    public void Handle(ZoneDeleteCommand input)
    {
        List<DbZone?> zones = _zoneRepository.GetAll().ToList();
        zones.ForEach(zone => Console.WriteLine(zone?.Name));
        Console.WriteLine("baguette");
        // Récupère l'entité existante depuis le DbContext
        var dbZone = _zoneRepository.GetById(input.id);
        if (dbZone == null)
            throw new EntityNotFoundException<Domain.Zone>(input.id);

        // Vérifie si l'utilisateur courant est autorisé
        if (dbZone.Id_user != int.Parse(input.userId))
            throw new UnauthorizedAccessException("You are not authorized to delete this zone.");

        // Supprime directement l'entité existante
        _zoneRepository.Delete(dbZone);
    }
}