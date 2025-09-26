using System.Linq;
using Application.user.queries.getAll;
using Application.utils;
using Infrastructure.Entities;
using Infrastructure.Repositories.Generic;

namespace Application.Features.Zone.Queries.GetAll
{
    public class ZoneEmptyGetAllHandler : IEmptyQueryHandler<ZoneGetallOutput>
    {
        private readonly IGenericRepository<DbZone> _repository;

        public ZoneEmptyGetAllHandler(IGenericRepository<DbZone> repository)
        {
            _repository = repository;
        }

        // Méthode standard pour l'interface (renvoie toutes les zones)
        public ZoneGetallOutput Handle()
        {
            return new ZoneGetallOutput
            {
                Zones = _repository.GetAll()
                    .Select(zone => new ZoneGetallOutput.ZoneDto
                    {
                        Id = zone.Id,
                        Name = zone.Name,
                        Id_user = zone.Id_user
                    })
                    .ToList()
            };
        }
        
    }
}