using Application.Services;
using Application.Shared.Exceptions;
using Application.user.commands.create;
using Application.utils;
using AutoMapper;
using Domain;
using Infrastructure.Entities;
using Infrastructure.Repositories.Generic;

namespace Application.Features.Zone.Commands.Create;

public class ZoneCreateHandler : ICommandHandler<ZoneCreateCommand, ZoneCreateOutput.ZoneCreateDto>
{
    private readonly IGenericRepository<DbZone?> _zoneRepository;
    private readonly IMapper _iMapper;

    public ZoneCreateHandler(IGenericRepository<DbZone?> userRepository, IMapper modelMapper)
    {
        _zoneRepository = userRepository;
        _iMapper = modelMapper;
    }
    
    public ZoneCreateOutput.ZoneCreateDto Handle(ZoneCreateCommand command)
    {
        int userId = int.Parse(command.user_id!);

        // Vérifie si une zone du même nom existe déjà pour cet utilisateur
        bool exists = _zoneRepository.GetAll()
            .Any(z => z != null && z.Name == command.Name && z.Id_user == userId);


        if (exists)
            throw new AlreadyExistsException<Domain.Zone>(command.Name);

        // Crée l'entité
        var dbZone = new DbZone
        {
            Id=0,
            Name = command.Name,
            Id_user = userId
        };

        // Ajoute et retourne la zone créée
        DbZone? dbZoneCreated = _zoneRepository.Add(dbZone);
        return _iMapper.Map<ZoneCreateOutput.ZoneCreateDto>(dbZoneCreated);
    }

}