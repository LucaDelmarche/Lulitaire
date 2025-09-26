using System.Net;
using Application.user.queries.getAll;
using Application.utils;
using AutoMapper;
using Azure.Core;
using Infrastructure.Entities;
using Infrastructure.Repositories.Generic;

namespace Application.Features.Zone.Queries.GetAll;

public class ZoneGetAllHandler : IQueryHandler<ZoneGetAllQuery,ZoneGetallOutput>
{
    private readonly IGenericRepository<DbZone> _repository;
    private readonly IMapper _iMapper;

    public ZoneGetAllHandler(IGenericRepository<DbZone> repository, IMapper iMapper)
    {
        _repository = repository;
        _iMapper = iMapper;
    }
    
    public ZoneGetallOutput Handle(ZoneGetAllQuery input)
    {
        IEnumerable<DbZone?> zones = _repository.GetAll();
        ZoneGetallOutput output = new ZoneGetallOutput
        {
            Zones = new List<ZoneGetallOutput.ZoneDto>()
        };
        foreach (DbZone? zone in zones)
        {
            ZoneGetallOutput.ZoneDto zoneDto = new ZoneGetallOutput.ZoneDto
            {
                Id = zone.Id,
                Name = zone.Name,
                Id_user = zone.Id_user
            };
            
            if (zoneDto.Id_user == int.Parse(input.id_user))
            {
                output.Zones.Add(zoneDto);
            }
        }

        return output;
    }
    
}