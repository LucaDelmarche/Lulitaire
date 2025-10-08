using Application.user.queries.getAll;
using Application.utils;
using Infrastructure.Entities;
using Infrastructure.Repositories.Generic;

namespace Application.Features.Item.Queries.GetAll
{
    public class ItemEmptyGetAllHandler : IEmptyQueryHandler<ItemGetallOutput>
    {
        private readonly IGenericRepository<DbItem> _repository;

        public ItemEmptyGetAllHandler(IGenericRepository<DbItem> repository)
        {
            _repository = repository;
        }

        // Méthode standard pour l'interface (renvoie toutes les zones)
        public ItemGetallOutput Handle()
        {
            return new ItemGetallOutput
            {
                Items = _repository.GetAll()
                    .Select(zone => new ItemGetallOutput.ItemDto
                    {
                        Id = zone.Id,
                        Name = zone.Name,
                        Id_user = zone.UserId,
                        Quantity = zone.Quantity,
                        Unit = zone.Unit,
                        ExpirationDate = zone.ExpritationData,
                        ZoneId = zone.ZoneId
                    })
                    .ToList()
            };
        }
        
    }
}